namespace Identity.Identity.Features.Refresh.Endpoint;

public record RefreshTokenResponse(string Token, DateTime TokenExpiryTime);