namespace Chamados.Service.Toolkit.Excecoes;

public sealed class BadRequestException : BaseException
{
    public BadRequestException(string pMessage)
        : base(pMessage)
    {
    }
}