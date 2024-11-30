using Reservations.Reservations.Features.CancelReservation.Handler;

namespace Reservations.Reservations.Features.CancelReservation.Endpoint;

public class CancelReservationEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/reservation/{reservationId:guid}", async (Guid reservationId, HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.User.Claims
                                 .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                             ?? throw new BadRequestException("User doesn't exist");

                var command = new CancelReservationCommand(userId, reservationId);
                var result = await sender.Send(command);
                return Results.NoContent();
            })
            .WithTags("Reservation")
            .RequireAuthorization();
    }
}