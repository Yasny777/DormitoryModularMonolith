using Dormitories.Dormitories.Features.GetRoomsInDormitory.Handler;

namespace Dormitories.Dormitories.Features.GetRoomsInDormitory.Endpoint;

public class GetRoomsInDormitoryEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/dormitory/{dormitoryId:guid}/room",
            async (Guid dormitoryId, [AsParameters] GetRoomsInDormitoryRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetRoomsInDormitoryQuery(
                    dormitoryId,
                    request.PageNumber,
                    request.PageSize,
                    request.SortBy,
                    request.SortDirection));

                var response = result.Adapt<GetRoomsInDormitoryResponse>();
                return Results.Ok(response);
            }).WithTags("Dormitory").RequireAuthorization();
    }
}