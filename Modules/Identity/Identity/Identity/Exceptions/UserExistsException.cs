namespace Identity.Identity.Exceptions;

public class UserExistsException : BadRequestException
{
    public UserExistsException(string? message) : base(message)
    {
    }
}