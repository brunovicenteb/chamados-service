namespace Chamados.Service.Toolkit.Excecoes;

public sealed class NotFoundException : BaseException
{
    public NotFoundException(string pMessage)
        : base(pMessage)
    {
    }
}