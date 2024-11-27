namespace Identity.Identity.Features.Refresh.Endpoint;

public record RefreshTokenResponse(string TokenToken, DateTime TokenExpiryTime);