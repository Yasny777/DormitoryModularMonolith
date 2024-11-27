namespace Identity.Identity.Services.TokenService;

public record JwtRefreshTokensWithExpiry(string Token, DateTime TokenExpiryTime, string RefreshToken, DateTime ExpiryRefreshTokenTime);