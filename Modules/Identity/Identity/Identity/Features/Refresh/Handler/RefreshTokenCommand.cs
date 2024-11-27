namespace Identity.Identity.Features.Refresh.Handler;

public record RefreshTokenCommand(string RefreshToken) : ICommand<RefreshTokenResult>;