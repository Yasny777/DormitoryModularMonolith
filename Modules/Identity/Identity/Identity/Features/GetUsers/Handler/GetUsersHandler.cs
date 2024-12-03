using Identity.Identity.Dto;
using Identity.Identity.Features.GetUsers.Endpoint;
using Shared.Contracts.CQRS;

namespace Identity.Identity.Features.GetUsers.Handler;

internal class GetUsersHandler(UserManager<AppUser> userManager)
    : IQueryHandler<GetAllUsersQuery, GetAllUsersResult>
{
    public async Task<GetAllUsersResult> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
    {
        var users = userManager.Users.ToList();

        var results = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await userManager.GetRolesAsync(user);
            results.Add(
                new UserDto(
                Guid.Parse(user.Id),
                user.UserName!,
                user.Email!,
                roles.ToList())
            );
        }

        return new GetAllUsersResult(results);
    }
}