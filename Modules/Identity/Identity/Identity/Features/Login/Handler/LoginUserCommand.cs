namespace Identity.Identity.Features.Login.Handler;

public record LoginUserCommand(string Email, string Password) : ICommand<LoginUserResult>
{
}