using Dormitories.Dormitories.Features.GetDormitories.Handler;

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
        }).WithTags("Dormitory").RequireAuthorization();
    }
}