﻿namespace Reservations.Reservations.Features.CreateReservation.Endpoint;

public class CreateReservationEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/reservation", async (HttpContext httpContext, CreateReservationRequest request, ISender sender) =>
            {
                var userId = httpContext.User.Claims
                                 .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value
                             ?? throw new BadRequestException("User doesn't exist");

                var command = new CreateReservationCommand(request.RoomId, Guid.Parse(userId));
                var result = await sender.Send(command);
                return Results.Created();
            })
            .WithTags("Reservation")
            .RequireAuthorization();
    }
}