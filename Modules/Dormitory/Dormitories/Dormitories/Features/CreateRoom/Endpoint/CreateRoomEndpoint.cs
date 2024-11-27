using Dormitories.Dormitories.Features.CreateRoom.Handler;

namespace Dormitories.Dormitories.Features.CreateRoom.Endpoint;

public class CreateRoomEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/dormitory/{dormitoryId}/room", async (Guid dormitoryId, CreateRoomRequest request, ISender sender) =>
        {
            var command = new CreateRoomCommand(dormitoryId, request.RoomDto);
            var result = await sender.Send(command);
            var response = result.Adapt<CreateRoomResponse>();
            return Results.Created($"dormitory/{dormitoryId}/room/{response.RoomId}", response);
        }).WithTags("Dormitory");
    }
}