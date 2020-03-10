using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;
using TMS.Persistence.Identity;

namespace TMS.Persistence
{
    public sealed class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            const string password = "1_Aadmin@test.ru";

            var admin = new AppUser
            {
                UserName = "admin@test.ru",
                Email = "admin@test.ru",
                EmailConfirmed = true,
            };

            var adminEmployee = new Employee
            {
                 FullName = "Mr. Admin",
                 ShortName = "M.A.",                 
            };

            var roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToList();
            
            foreach(var role in roles)
            {
                if (await roleManager.FindByNameAsync(role.ToString()) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                }
            }

            if (await userManager.FindByEmailAsync(admin.Email) == null)
            {
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    adminEmployee.AppUserId = admin.Id;
                    await userManager.AddToRoleAsync(admin, UserRole.Admin.ToString());
                    await context.Employees.AddAsync(adminEmployee);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
