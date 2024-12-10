namespace Identity.Identity.Features.Logout.Handler;

internal record LogoutUserCommand(string UserId) : ICommand<LogoutUserResult>;