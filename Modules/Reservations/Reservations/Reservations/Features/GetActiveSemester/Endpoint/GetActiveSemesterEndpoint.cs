using Reservations.Reservations.Features.GetActiveSemester.Handler;

namespace Reservations.Reservations.Features.GetActiveSemester.Endpoint;

public class GetActiveSemesterEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/semester/active", async (ISender sender) =>
        {
            var query = new GetActiveSemesterQuery();
            var result = await sender.Send(query);
            return Results.Ok(result);
        }).WithTags("Semester").RequireAuthorization();
    }
}