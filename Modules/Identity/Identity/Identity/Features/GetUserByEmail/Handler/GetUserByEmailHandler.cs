using Identity.Identity.Features.GetUserByEmail.Endpoint;
using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.GetUserByEmail.Handler;

internal class GetUserByEmailHandler(UserManager<AppUser> userManager)
    : IQueryHandler<GetUserByEmailQuery, GetUserByEmailResult>
{
    public async Task<GetUserByEmailResult> Handle(GetUserByEmailQuery query, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(query.Email);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with email {query.Email} not found.");
        }

        var roles = await userManager.GetRolesAsync(user);

        return new GetUserByEmailResult(
            Guid.Parse(user.Id),
            user.UserName!,
            user.Email!,
            roles
        );
    }
}