using Identity.Identity.Features.Login.Handler;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Features.Endpoints;

namespace Identity.Identity.Features.Login.Endpoint;

public class LoginUserEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login",
            async (HttpContext httpContext, LoginUserRequest request, ISender sender) =>
            {
                var command = request.Adapt<LoginUserCommand>();
                var result = await sender.Send(command);

                httpContext.Response.Cookies.Append("App_Dormitory_Refresh", result.RefreshToken, new CookieOptions()
                {
                    HttpOnly = true,
                    Expires = result.ExpiryRefreshTokenTime,
                    Secure = false, // change to true in production through HTTPS
                    SameSite = SameSiteMode.Lax, // Pozwala na żądania cross-origin
                });

                return Results.Ok(new LoginUserResponse(result.Token, result.TokenExpiryTime));
            }).WithTags("Identity");
    }
}