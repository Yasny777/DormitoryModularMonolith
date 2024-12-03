using Mapster;
using Reservations.Reservations.Features.CreateSemester.Handler;
using Shared.Constants;

namespace Reservations.Reservations.Features.CreateSemester.Endpoint;

public class CreateSemesterEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/semester", async (CreateSemesterRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateSemesterCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateSemesterResponse>();
            return Results.Created($"/semester/{response.Id}", response);
        }).WithTags("Semester").RequireAuthorization(c => c.RequireRole(AppRoles.Admin));
    }
}