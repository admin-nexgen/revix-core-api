using System;

namespace Revix.Core.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base()
    {
    }
}