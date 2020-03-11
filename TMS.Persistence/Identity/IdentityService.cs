using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TMS.Application.Common.Models;
using TMS.Application.Common.Interfaces;
using System.Collections.Generic;

namespace TMS.Persistence.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;

        public IdentityService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsUserExistAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<Result> AddRoleToUserAsync(string userId, string role)
        {
            IdentityResult result;

            var appUser = await _userManager.Users.FirstAsync(u => u.Id == userId);

            if (appUser != null)
            {
                result = await _userManager.AddToRoleAsync(appUser, role);
            }
            else
            {
                result = new IdentityResult();
            }

            return result.ToApplicationResult();
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new AppUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<string> GetUserRoleAsync(string userId)
        {
            var appUser = await _userManager.Users.FirstAsync(u => u.Id == userId);

            var result = await _userManager.GetRolesAsync(appUser);

            return result.FirstOrDefault();
        }

        public async Task<List<(string appUserId, string role)>> FetchUserRolesAsync()
        {
            var appUsers = await _userManager.Users.ToArrayAsync();

            var list = new List<(string appUserId, string role)>(appUsers.Count());

            foreach (var user in appUsers)
            {
                var role = await _userManager.GetRolesAsync(user);

                list.Add((user.Id, role.FirstOrDefault()));
            }

            return list;
        }
    }
}
