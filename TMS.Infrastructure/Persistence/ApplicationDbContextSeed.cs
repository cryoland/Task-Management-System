using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TMS.Infrastructure.Identity;

namespace TMS.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string password = "1_Aadmin@test.ru";
            var admin = new AppUser
            {
                UserName = "admin@test.ru",
                Email = "admin@test.ru",
                EmailConfirmed = true,
            };

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }

            if (await roleManager.FindByNameAsync("manager") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("manager"));
            }

            if (await roleManager.FindByNameAsync("developer") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("developer"));
            }

            if (await userManager.FindByEmailAsync(admin.Email) == null)
            {
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
