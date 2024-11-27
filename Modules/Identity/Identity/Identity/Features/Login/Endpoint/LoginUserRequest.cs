namespace Identity.Identity.Features.Login.Endpoint;

public record LoginUserRequest(string Email, string Password)
{
}