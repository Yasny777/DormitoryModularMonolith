using Shared.Constants;

namespace Identity.Data.Seed;

public static class InitialData
{
    public static IEnumerable<AppRole> Roles =>
    [
        new AppRole(){Name = AppRoles.Candidate},
        new AppRole(){Name = AppRoles.Admin},
        new AppRole(){Name = AppRoles.Student}
    ];

    public static IEnumerable<AppUser> Users =>
    [
        
    ];
}