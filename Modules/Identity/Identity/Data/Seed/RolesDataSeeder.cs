using Identity.Identity.Constants;
using Identity.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Data.Seed;

namespace Identity.Data.Seed;

public class RolesDataSeeder(RoleManager<AppRole> roleManager) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
        if (!await roleManager.Roles.AnyAsync())
        {
            foreach (var role in InitialData.Roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}