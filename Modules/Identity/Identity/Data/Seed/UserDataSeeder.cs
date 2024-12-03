using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Data.Seed;

namespace Identity.Data.Seed;

public class UserDataSeeder(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager) : IDataSeeder
{
    public async Task SeedAllAsync()
    {
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