using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.Register.Handler;

public record RegisterUserCommand(string Email, string Password, string PasswordConfirm) : ICommand<RegisterUserResult>
{
}