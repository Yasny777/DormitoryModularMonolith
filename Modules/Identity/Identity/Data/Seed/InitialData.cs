namespace Identity.Data.Seed;

public static class InitialData
{
    public static IEnumerable<AppRole> Roles =>
    [
        new AppRole(){Name = AppRoles.Candidate},
        new AppRole(){Name = AppRoles.Admin},
        new AppRole(){Name = AppRoles.Student},
        new AppRole(){Name = AppRoles.Disabled}
    ];

    public static IEnumerable<AppUser> Users =>
    [
        
    ];
}