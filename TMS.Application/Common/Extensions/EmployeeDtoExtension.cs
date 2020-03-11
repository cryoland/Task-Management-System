using System.Linq;
using System.Collections.Generic;
using TMS.Application.Common.Interfaces;
using TMS.Application.Employees.Queries.GetEmployeeList;
using System.Threading.Tasks;

namespace TMS.Application.Common.Extensions
{
    public static class EmployeeDtoExtension
    {
        public static async Task<List<EmployeeDto>> ProjectUserAppRoles(this List<EmployeeDto> employeeDtos, IIdentityService identityService)
        {
            var userRoles = await identityService.FetchUserRolesAsync();

            for (int i = 0; i < employeeDtos.Count; i++)
            {
                employeeDtos[i].RoleName = userRoles.FirstOrDefault(ur => ur.appUserId == employeeDtos[i].AppUserId).role;
            }

            return employeeDtos;
        }
    }
}
