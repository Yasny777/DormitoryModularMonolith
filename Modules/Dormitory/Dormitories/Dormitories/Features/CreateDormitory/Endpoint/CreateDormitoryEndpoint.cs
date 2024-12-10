using Dormitories.Dormitories.Features.CreateDormitory.Handler;

namespace Dormitories.Dormitories.Features.CreateDormitory.Endpoint;

public class CreateDormitoryEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/dormitory", async (CreateDormitoryRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateDormitoryCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateDormitoryResponse>();
            return Results.Created($"/dormitory/{response.Id}", response);
        }).WithTags("Dormitory").RequireAuthorization(c => c.RequireRole(AppRoles.Admin));
    }
}