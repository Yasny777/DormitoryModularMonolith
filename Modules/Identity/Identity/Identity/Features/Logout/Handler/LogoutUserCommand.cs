using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.Logout.Handler;

public record LogoutUserCommand(string UserId) : ICommand<LogoutUserResult>;