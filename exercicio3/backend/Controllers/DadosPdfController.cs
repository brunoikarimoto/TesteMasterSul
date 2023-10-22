using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DadosPdfController : ControllerBase
{
    // Não salvo em lugar algum os arquivos enviados anteriormente, se der f5 os anteriores somem, fica "salvo" só no front
    // Essa função, ela recebe os arquivos, abre pega todo o texto e eu seleciono o que é pedido, no final retorna lista com todos uma lista de DadosPdf, que tem o nome do arquivo e as coisas pedidas
    // Bem hardcoded também se o arquivo mudar um pouco já não funciona
    [HttpPost]
    [Route("upload")]
    public IActionResult UploadPdf([FromForm]List<IFormFile> pdfFiles){
         List<DadosPdf> listaTextos = new List<DadosPdf>();

        try
        {
            if(pdfFiles is null || pdfFiles.Count == 0){
                return BadRequest("Nenhum arquivo pdf enviado.");
            }

            foreach(var pdfFile in pdfFiles){
                try
                {
                    if(pdfFile is null || pdfFile.Length == 0 || pdfFile.ContentType != "application/pdf")
                    {
                        return BadRequest("Envie um arquivo válido.");
                    }

                    var text = ExtractTextFromPdf(pdfFile.OpenReadStream());

                    string[] linhas = text.Split('\n');

                    int i = 0;

                    int valorTotalDaNota = -1;
                    int infoAdInicio = -1;
                    int infoAdFim = -1;
                    int natureza = -1;

                    // Aqui vou linha por linha procurando as informações
                    // Quando encontra eu salvo a linha nas variáveis que estão acima
                    foreach(string linha in linhas)
                    {
                        Console.WriteLine(linha);
                        if(linha.EndsWith("VALOR TOTAL DA NOTA"))
                        {
                            valorTotalDaNota = i + 1;
                        }
                        if(linha.StartsWith("DADOS ADICIONAIS"))
                        {
                            infoAdInicio = i + 1;
                        }
                        if(linha.StartsWith("DADOS DA AIDF"))
                        {
                            infoAdFim = i;
                        }
                        if(linha.StartsWith("NATUREZA DA OPERAÇÃO"))
                        {
                            natureza = i + 1;
                        }

                        i++;
                    }

                    // É onde eu salvo a string depois de um leve tratamento
                    string adicionais = ""; // Dados Adicionais
                    string sub = ""; // Sub pois é uma substring da linha de Natureza da Operação
                    string valorNota = ""; // Valor Total da Nota

                    if(valorTotalDaNota != -1)
                    {
                        // Faço esse split pois tem vezes que tem mais valores nessa linha
                        string[] aux = linhas[valorTotalDaNota].Split(" ");
                        valorNota = aux[aux.Length - 1]; // Pego o último do split
                    } 

                    if(infoAdFim != -1 && infoAdInicio != -1)
                    {
                        // Pego todas as linhas dos Dados Adicionais e junto em uma só
                        for(int aux = infoAdInicio; aux < infoAdFim; aux++)
                        {
                            adicionais += linhas[aux];
                        }
                    }

                    if(natureza != -1)
                    {
                        // Vou char por char até achar um número, pois é o padrão que notei na linha
                        foreach(char c in linhas[natureza])
                        {
                            if(Char.IsDigit(c))
                            {
                                break;
                            }

                            sub += c;
                        }
                    }

                    DadosPdf dados = new DadosPdf
                    {
                        NomeDoArquivo = pdfFile.FileName,
                        NaturezaDaOperacao = sub,
                        DadosAdicionais = adicionais,
                        ValorTotalDaNota = valorNota
                    };
                    
                    // Isso seria o fim de um arquivo então adiciono as informações na lista que criei no começo
                    listaTextos.Add(dados);
                }catch(Exception e)
                {
                    return BadRequest($"Algo deu errado: {e.Message}");
                }
            }

            // Fim de todos os arquivos então retorno o Array inteiro
            return Ok(listaTextos);            
        }
        catch(Exception e)
        {
            return BadRequest($"Algo deu errado: {e.Message}");
        }
    }

    // Função para extrair texto do pdf
    private string ExtractTextFromPdf(Stream pdfStream){
        using (var reader = new PdfReader(pdfStream)){
            var text = new StringBuilder();

            for(int i = 1; i <= reader.NumberOfPages; i++){
                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
            }

            return text.ToString();
        }
    }
}