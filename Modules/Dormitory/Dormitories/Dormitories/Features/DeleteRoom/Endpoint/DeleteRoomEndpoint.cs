using Dormitories.Dormitories.Features.DeleteRoom.Handler;

namespace Dormitories.Dormitories.Features.DeleteRoom.Endpoint;

public class DeleteRoomEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("dormitory/{dormitoryId:guid}/room/{roomId:guid}", async (Guid dormitoryId, Guid roomId, ISender sender) =>
            {
                var command = new DeleteRoomCommand(dormitoryId, roomId);
                await sender.Send(command);
                return Results.NoContent();
            })
            .WithTags("Dormitory")
            .RequireAuthorization();
    }
}