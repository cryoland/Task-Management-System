using TMS.Application.Common.Models;
using System.Threading.Tasks;

namespace TMS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsUserExistAsync(string parameter);

        Task<Result> AddRoleToUserAsync(string userId, string role);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> DeleteUserAsync(string userId);
    }
}
