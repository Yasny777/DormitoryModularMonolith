using Dormitories.Contracts.Dto;
using Dormitories.Dormitories.Features.UpdateRoom.Handler;
using Shared.Contracts.CQRS;

namespace Dormitories.Dormitories.Features.UpdateRoom.Endpoint;

public class UpdateRoomEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("dormitory/{dormitoryId:guid}/room/{roomId:guid}", async (Guid dormitoryId, Guid roomId, UpdateRoomRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateRoomCommand>() with { DormitoryId = dormitoryId, RoomId = roomId };
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithTags("Dormitory")
            .RequireAuthorization();
    }
}

public record UpdateRoomCommand(Guid DormitoryId, Guid RoomId, string? Number, int? Capacity, decimal? Price) : ICommand<UpdateRoomResult>;
public record UpdateRoomRequest(string? Number, int? Capacity, decimal? Price);
public record UpdateRoomResult(RoomDto Room);
