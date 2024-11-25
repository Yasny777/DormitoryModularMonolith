using Identity.Identity.Constants;
using Identity.Identity.Models;

namespace Identity.Data.Seed;

public static class InitialData
{
    public static IEnumerable<AppRole> Roles =>
    [
        new AppRole(AppRoles.Admin), new AppRole(AppRoles.Manager), new AppRole(AppRoles.Student),
        new AppRole(AppRoles.Candidate)
    ];
}