using Identity.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Shared.Features.Endpoints;

namespace Identity.Identity.Features.Register;

public class RegisterEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (HttpContext httpContext, UserManager<AppUser> userManager) =>
        {

        });
    }
}