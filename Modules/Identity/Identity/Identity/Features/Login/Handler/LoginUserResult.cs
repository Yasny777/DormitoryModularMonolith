namespace Identity.Identity.Features.Login.Handler;

public record LoginUserResult(string Token, DateTime TokenExpiryTime, string RefreshToken, DateTime ExpiryRefreshTokenTime)
{
}