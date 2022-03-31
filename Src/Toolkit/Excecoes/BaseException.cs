namespace Chamados.Service.Toolkit.Excecoes;

public abstract class BaseException : Exception
{
    public BaseException(string pMessage)
        : base(pMessage)
    {

    }
}