﻿using Identity.Identity.Constants;
using Identity.Identity.Models;

namespace Identity.Data.Seed;

public static class InitialData
{
    public static IEnumerable<AppRole> Roles =>
    [
        new AppRole(){Name = AppRoles.Candidate}
    ];
}