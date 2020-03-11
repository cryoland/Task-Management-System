using TMS.Application.Common.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TMS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<string> GetUserRoleAsync(string userId);

        Task<bool> IsUserExistAsync(string parameter);

        Task<Result> AddRoleToUserAsync(string userId, string role);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);

        Task<List<(string appUserId, string role)>> FetchUserRolesAsync();
    }
}
