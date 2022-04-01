namespace Chamados.Service.Toolkit.Web;

public class ErrorResponse
{
    public ErrorResponse(string id)
    {
        ID = id;
        Moment = DateTime.Now;
        Message = "Um erro inesperado aconteceu no servidor. Por favor entre em contato com o suporte.";
    }

    public string ID { get; set; }
    public DateTime Moment { get; set; }
    public string Message { get; set; }
}