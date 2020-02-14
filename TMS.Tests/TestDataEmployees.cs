using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Tests
{
    public static partial class TestData
    {
        public static async Task<List<Employees>> EmployeeGetListAsync()
        {
            return await Task.FromResult(new List<Employees>()
            {
               new Employees
               {
                   Id = 1,
                   ShortName = "S.N.",
                   FullName = EmployeeRole.Admin.ToString(),
                   Email = "a.rybakov@test.ru",
                   Password = "rewtydfggdsfgdfsg",
                   Role = new Role { Id = 0, Name = EmployeeRole.Admin },
                   RoleId = 1
               },
               new Employees
               {
                   Id = 2,
                   ShortName = "E.N.",
                   FullName = EmployeeRole.Developer.ToString(),
                   Email = "e.efremov@test.ru",
                   Password = "6yt5uhgfshfgh",
                   Role = new Role { Id = 0, Name = EmployeeRole.Developer },
                   RoleId = 3
               },
               new Employees
               {
                   Id = 3,
                   ShortName = "I.G.",
                   FullName = EmployeeRole.Manager.ToString(),
                   Email = "i.dmitriev@test.ru",
                   Password = "retwrjuiytjghf",
                   Role = new Role { Id = 0, Name = EmployeeRole.Manager },
                   RoleId = 2
               },
            });
        }
    }
}
