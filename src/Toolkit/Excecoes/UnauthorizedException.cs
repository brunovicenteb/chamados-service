namespace Chamados.Service.Toolkit.Excecoes;

public sealed class UnauthorizedException : BaseException
{
    public UnauthorizedException(string pMessage)
        : base(pMessage)
    {
    }
}