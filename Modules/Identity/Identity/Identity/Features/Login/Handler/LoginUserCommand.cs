using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.Login.Handler;

internal record LoginUserCommand(string Email, string Password) : ICommand<LoginUserResult>
{
}