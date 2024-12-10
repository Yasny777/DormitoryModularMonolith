using Identity.Identity.Features.GetUsers.Handler;

namespace Identity.Identity.Features.GetUsers.Endpoint;

public class GetUsersEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/user", async (ISender sender) =>
        {
            var query = new GetAllUsersQuery();
            var result = await sender.Send(query);

            return Results.Ok(result);
        }).WithTags("User").RequireAuthorization();
    }
}