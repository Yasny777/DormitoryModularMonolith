namespace Identity.Identity.Features.Register.Endpoint;

public record RegisterUserRequest(string Email, string Password, string PasswordConfirm)
{
}