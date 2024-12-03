using Mapster;
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
            var response = result.Adapt<GetActiveSemesterResponse>();
            return Results.Ok(response);
        }).WithTags("Semester").RequireAuthorization();
    }
}