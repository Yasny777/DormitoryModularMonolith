using Carter;
using Dormitories.Dormitories.Features.GetDormitories.Handler;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Exceptions;
using Shared.Features.Endpoints;

namespace Dormitories.Dormitories.Features.GetDormitories.Endpoint;

public class GetDormitoriesEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/dormitory", async (ISender sender) =>
        {
            var result = await sender.Send(new GetDormitoriesQuery());
            var response = result.Adapt<GetDormitoriesResponse>();
            return Results.Ok(response);
        });
    }
}