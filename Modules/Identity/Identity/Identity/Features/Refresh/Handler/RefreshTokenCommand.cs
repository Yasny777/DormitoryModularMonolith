using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.Refresh.Handler;

internal record RefreshTokenCommand(string RefreshToken) : ICommand<RefreshTokenResult>;