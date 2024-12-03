using Reservations.Reservations.Features.GetUserReservationsById.Handler;

namespace Reservations.Reservations.Features.GetUserReservationsById.Endpoint;

public class GetUserReservationsByIdEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reservation/user/{userId:guid}", async (Guid userId, ISender sender) =>
            {
                var query = new GetUserReservationsByIdQuery(userId);
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .WithTags("Reservation")
            .RequireAuthorization();
    }
}