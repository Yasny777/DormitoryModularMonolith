using Identity.Identity.Features.Register.Handler;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Features.Endpoints;

namespace Identity.Identity.Features.Register.Endpoint;

public class RegisterUserEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (RegisterUserRequest request, ISender sender) =>
        {
            var command = request.Adapt<RegisterUserCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<RegisterUserResponse>();
        }).WithTags("Identity");
    }
}