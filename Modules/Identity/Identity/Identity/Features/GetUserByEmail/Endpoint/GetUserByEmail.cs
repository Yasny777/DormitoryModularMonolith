using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Shared.Contracts.CQRS;
using Shared.Features.Endpoints;

namespace Identity.Identity.Features.GetUserByEmail.Endpoint;

public class GetUserByEmailEndpoint : PrefixedCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/user/email/{email}", async (string email, ISender sender) =>
        {
            var query = new GetUserByEmailQuery(email);
            var result = await sender.Send(query);
            var response = result.Adapt<GetUserByEmailResponse>();
            return Results.Ok(response);
        }).WithTags("User").RequireAuthorization();
    }
}

public record GetUserByEmailQuery(string Email) : IQuery<GetUserByEmailResult>;
public record GetUserByEmailResult(Guid Id, string UserName, string Email, IList<string> Roles);
public record GetUserByEmailResponse(Guid Id, string UserName, string Email, IList<string> Roles);