using Identity.Identity.Features.GetUserByEmail.Handler;

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