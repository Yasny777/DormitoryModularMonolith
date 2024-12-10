using Dormitories.Contracts.Dormitories.GetRoomById;

namespace Dormitories.Dormitories.Features.GetRoomById.Endpoint;

public class GetRoomByIdEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/dormitory/{dormitoryId}/room/{roomId}", async ([AsParameters] GetRoomByIdRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetRoomByIdQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetRoomByIdResponse>();

            return Results.Ok(response);
        }).WithTags("Dormitory").RequireAuthorization();
    }
}