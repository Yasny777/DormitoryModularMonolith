﻿using System.Security.Claims;
using Identity.Identity.Features.Logout.Handler;

namespace Identity.Identity.Features.Logout.Endpoint;

public class LogoutUserEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/logout",
            async (HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.User.Claims
                                 .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                             ?? throw new BadRequestException("User doesn't exist");

                var result = await sender.Send(new LogoutUserCommand(userId));

                httpContext.Response.Cookies.Delete("App_Dormitory_Refresh", new CookieOptions()
                {
                    HttpOnly = true,
                    Secure = false, // change to true in production through HTTPS
                    SameSite = SameSiteMode.Lax,
                });

                return Results.Ok();
            }).WithTags("Identity").RequireAuthorization();
    }
}