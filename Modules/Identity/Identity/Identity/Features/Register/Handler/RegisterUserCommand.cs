namespace Identity.Identity.Features.Register.Handler;

internal record RegisterUserCommand(string Email, string Password, string PasswordConfirm) : ICommand<RegisterUserResult>
{
}