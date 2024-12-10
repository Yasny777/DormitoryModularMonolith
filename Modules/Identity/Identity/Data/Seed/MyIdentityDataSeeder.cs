using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Data.Seed;

namespace Identity.Data.Seed;

public class MyIdentityDataSeeder(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager) : IDataSeeder
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

        var user = await userManager.FindByEmailAsync("admin@admin.com");
        if (user == null)
        {
            var user1 = new AppUser() { Email = "admin@admin.com", UserName = "admin@admin.com" };
            await userManager.CreateAsync(user1,
                "Admin!234");
            await userManager.AddToRoleAsync(user1, AppRoles.Admin);
        }
    }
}