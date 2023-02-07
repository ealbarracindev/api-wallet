
namespace wallet.core.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message) : base(message)
    {
    }
}