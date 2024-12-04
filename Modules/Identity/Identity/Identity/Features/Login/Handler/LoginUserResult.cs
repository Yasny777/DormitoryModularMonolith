namespace Identity.Identity.Features.Login.Handler;

internal record LoginUserResult(string Token, DateTime TokenExpiryTime, string RefreshToken, DateTime ExpiryRefreshTokenTime)
{
}