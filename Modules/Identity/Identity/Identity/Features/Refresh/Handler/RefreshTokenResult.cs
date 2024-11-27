namespace Identity.Identity.Features.Refresh.Handler;

public record RefreshTokenResult(string Token, DateTime TokenExpiryTime, string RefreshToken, DateTime ExpiryRefreshTokenTime)
{
}