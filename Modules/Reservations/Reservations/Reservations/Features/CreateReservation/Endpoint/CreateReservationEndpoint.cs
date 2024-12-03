namespace Reservations.Reservations.Features.CreateReservation.Endpoint;

public class CreateReservationEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/reservation/{roomId:guid}",
                async (HttpContext httpContext, Guid roomId, CreateReservationRequest request, ISender sender) =>
                {
                    var userId = httpContext.User.Claims
                                     .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                                 ?? throw new BadRequestException("User doesn't exist");

                    var command = new CreateReservationCommand(roomId, Guid.Parse(userId), request.SemesterName);
                    var result = await sender.Send(command);
                    return Results.Created();
                })
            .WithTags("Reservation")
            .RequireAuthorization();
    }
}