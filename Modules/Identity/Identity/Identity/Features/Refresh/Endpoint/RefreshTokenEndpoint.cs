using Identity.Identity.Features.Refresh.Handler;

namespace Identity.Identity.Features.Refresh.Endpoint;

public class RefreshTokenEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/refresh", async (HttpContext httpContext, ISender sender) =>
        {
            var refreshToken = httpContext.Request.Cookies["App_Dormitory_Refresh"];
            if (string.IsNullOrEmpty(refreshToken))
                return Results.BadRequest("No refresh token provided");

            var command = new RefreshTokenCommand(refreshToken);
            var result = await sender.Send(command);

            // Ustawienie nowego Refresh Token w ciasteczku
            httpContext.Response.Cookies.Append("App_Dormitory_Refresh", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = result.ExpiryRefreshTokenTime,
                Secure = true, // Wymagaj HTTPS w produkcji
                SameSite = SameSiteMode.Lax,
            });

            return Results.Ok(new RefreshTokenResponse(result.Token, result.TokenExpiryTime));
        }).WithTags("Identity");
    }
}