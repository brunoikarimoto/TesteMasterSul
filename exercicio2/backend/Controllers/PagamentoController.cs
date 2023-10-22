using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Linq;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PagamentoController : ControllerBase
{
    // Forma para guardar todos os pagamentos que vierem do .xlsx
    private readonly List<Pagamento> pagamentos = new List<Pagamento>();

    // Assim que instanciado ele realiza a leitura do arquivo e pega os dados
    public PagamentoController(IConfiguration configuration)
    {
        string filePath = configuration["ArquivosExcel:Caminho"];

        using(var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        using (var package = new ExcelPackage(stream))
        {
            ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            // Isso ta bem hardcoded mas não sei outra maneira de realizar
            // Se mudar a ordem das colunas por exemplo já da errado
            for(int row = 1; row <= rowCount; row++){
                var id = Convert.ToInt32(worksheet.Cells[row, 1].Text);
                var idUsuario = Convert.ToInt32(worksheet.Cells[row, 2].Text);
                var tipo = worksheet.Cells[row, 3].Text;
                float valor;

                if(float.TryParse(worksheet.Cells[row, 4].Text, out valor)){
                    pagamentos.Add(new Pagamento {Valor = valor, Tipo = tipo, Id = id, IdUsuario = idUsuario});
                }
            }
        }
    }

    // Rota para pegar todos os pagamentos
    [HttpGet]
    [Route("/pagamento")]
    public ActionResult<IEnumerable<Pagamento>> Get()
    {
        return pagamentos;
    }

    // Rota para pegar todos pagamentos de um usuário apenas
    [HttpGet]
    [Route("/pagamento/{idUsuario}")]
    public ActionResult<IEnumerable<Pagamento>> BuscarUsuarioId([FromRoute] int idUsuario)
    {
        var pagamento = pagamentos.Where(u => u.IdUsuario == idUsuario).ToList();

        if(pagamento.Count > 0) return pagamento;

        return BadRequest("Id não encontrado");
    }
}
