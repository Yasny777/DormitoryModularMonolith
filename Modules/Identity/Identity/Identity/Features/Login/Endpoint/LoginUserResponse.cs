namespace Identity.Identity.Features.Login.Endpoint;

public record LoginUserResponse(string TokenToken, DateTime TokenExpiryTime);