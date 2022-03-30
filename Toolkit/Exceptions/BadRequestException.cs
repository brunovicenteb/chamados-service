﻿namespace Chamados.Service.Toolkit.Exceptions;

public sealed class BadRequestException : BaseException
{
    public BadRequestException(string pMessage)
        : base(pMessage)
    {
    }
}