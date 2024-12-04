namespace Identity.Identity.Features.Refresh.Handler;

internal record RefreshTokenResult(string Token, DateTime TokenExpiryTime, string RefreshToken, DateTime ExpiryRefreshTokenTime)
{
}