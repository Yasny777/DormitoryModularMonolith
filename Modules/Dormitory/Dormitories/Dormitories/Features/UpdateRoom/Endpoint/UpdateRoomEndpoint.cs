using Dormitories.Dormitories.Features.UpdateRoom.Handler;

namespace Dormitories.Dormitories.Features.UpdateRoom.Endpoint;

public class UpdateRoomEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("dormitory/{dormitoryId:guid}/room/{roomId:guid}", async (Guid dormitoryId, Guid roomId, UpdateRoomRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateRoomCommand>() with { DormitoryId = dormitoryId, RoomId = roomId };
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithTags("Dormitory")
            .RequireAuthorization();
    }
}