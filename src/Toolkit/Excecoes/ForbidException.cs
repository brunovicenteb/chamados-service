namespace Chamados.Service.Toolkit.Excecoes;

public sealed class ForbidException : BaseException
{
    public ForbidException(string pMessage)
        : base(pMessage)
    {
    }
}