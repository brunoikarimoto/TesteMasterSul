namespace backend;

// São as colunas presentes no meu arquivo .xlsx (arquivo: Data/data.xlsx)
public class Pagamento
{
    public float Valor {get; set;}
    public string Tipo {get; set;}
    public int Id {get; set;}
    public int IdUsuario {get; set;}
}
