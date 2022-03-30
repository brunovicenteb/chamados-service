namespace Chamados.Service.Toolkit.Exceptions;

public abstract class BaseException : Exception
{
    public BaseException(string pMessage)
        : base(pMessage)
    {
      
    }
}