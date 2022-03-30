﻿namespace Chamados.Service.Toolkit.Exceptions;

public sealed class UnauthorizedException : BaseException
{
    public UnauthorizedException(string pMessage)
        : base(pMessage)
    {
    }
}