﻿using Dormitories.Dormitories.Features.UpdateDormitory.Handler;

namespace Dormitories.Dormitories.Features.UpdateDormitory.Endpoint;

public class UpdateDormitoryEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/dormitory/{dormitoryId:guid}", async (Guid dormitoryId, UpdateDormitoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateDormitoryCommand>() with { DormitoryId = dormitoryId };
                var result = await sender.Send(command);
                return Results.Ok(result);
            })
            .WithTags("Dormitory")
            .RequireAuthorization();
    }
}