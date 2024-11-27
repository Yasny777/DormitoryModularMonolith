using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Reservations.Reservations.Features.CreateReservation.Handler;
using Shared.Exceptions;
using Shared.Features.Endpoints;

namespace Reservations.Reservations.Features.CreateReservation.Endpoint;

public class CreateReservationEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/reservation", async (HttpContext httpContext, CreateReservationRequest request, ISender sender) =>
        {
            var userId = httpContext.User.Identity?.Name ?? throw new BadRequestException("User doesnt exist");

            var command = new CreateReservationCommand(request.RoomId, Guid.Parse(userId));
            var result = await sender.Send(command);

        }).WithTags("Reservation");
    }
}