using Reservations.Reservations.Features.UpdateReservationDatesForUser.Handler;

namespace Reservations.Reservations.Features.UpdateReservationDatesForUser.Endpoint;

public class UpdateReservationDatesForUserEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/reservation/{reservationId:guid}",
                async (Guid reservationId, UpdateReservationDatesForUserRequest request, ISender sender) =>
                {
                    var command =
                        new UpdateReservationDatesForUserCommand(reservationId, request.NewStartDate,
                            request.NewEndDate);

                    var result = await sender.Send(command);
                    return Results.Ok(result);
                })
            .WithTags("Reservation")
            .RequireAuthorization();
    }
}