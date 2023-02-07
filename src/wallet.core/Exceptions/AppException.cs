using System;

namespace wallet.core.Exceptions;

public class AppException: Exception
{
    public AppException(string message) : base(message)
    {
    }
}
