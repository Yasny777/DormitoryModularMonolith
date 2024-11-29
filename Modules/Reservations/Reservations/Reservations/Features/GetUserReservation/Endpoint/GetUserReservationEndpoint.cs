using Reservations.Reservations.Features.GetUserReservation.Handler;

namespace Reservations.Reservations.Features.GetUserReservation.Endpoint;

public class GetUserReservationEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/reservation", async (HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.User.Claims
                                 .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                             ?? throw new BadRequestException("User doesn't exist");

                var query = new GetUserReservationQuery(userId);
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .WithTags("Reservation")
            .RequireAuthorization();
    }
}