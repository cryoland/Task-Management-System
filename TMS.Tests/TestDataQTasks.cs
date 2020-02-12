using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Tests
{
    public static class TestData
    {
        public static async Task<List<QTask>> GetAsyncQTasksList()
        {
            return await Task.FromResult(new List<QTask>()
            {
               new QTask 
               { 
                   Id = 1, 
                   Name = "Name1", 
                   Description = "Admin_To_Manager", 
                   Priority = TaskPriority.Default,
                   Assignee = new Employees { FullName = EmployeeRole.Manager.ToString() }, 
                   AssigneeId = 1,
                   Reporter = new Employees { FullName = EmployeeRole.Admin.ToString() },
                   ReporterId = 3, 
                   Status = QTaskStatus.New 
               },
               new QTask 
               { 
                   Id = 2, 
                   Name = "Name2", 
                   Description = "Dev_To_Admin", 
                   Priority = TaskPriority.Major,
                   Assignee = new Employees { FullName = EmployeeRole.Admin.ToString() },
                   AssigneeId = 2,
                   Reporter = new Employees { FullName = EmployeeRole.Developer.ToString() },
                   ReporterId = 2, 
                   Status = QTaskStatus.New 
               },
               new QTask
               { 
                   Id = 3, 
                   Name = "Name3", 
                   Description = "Manager_To_Dev", 
                   Priority = TaskPriority.Default,                        
                   Assignee = new Employees { FullName = EmployeeRole.Developer.ToString() },                        
                   AssigneeId = 3,                        
                   Reporter = new Employees { FullName = EmployeeRole.Manager.ToString() },                        
                   ReporterId = 1, 
                   Status = QTaskStatus.New 
               },
               new QTask
               {
                   Id = 4,
                   Name = "Name3",
                   Description = "Dev_To_Manager",
                   Priority = TaskPriority.Default,
                   Assignee = new Employees { FullName = EmployeeRole.Manager.ToString() },
                   AssigneeId = 1,
                   Reporter = new Employees { FullName = EmployeeRole.Developer.ToString() },
                   ReporterId = 3,
                   Status = QTaskStatus.New
               },
            });
        }
        public static async Task<List<Employees>> GetAsyncEmployeeList()
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
                   RoleId = 1
               },
               new Employees
               {
                   Id = 2,
                   ShortName = "E.N.",
                   FullName = EmployeeRole.Developer.ToString(),
                   Email = "e.efremov@test.ru",
                   Password = "6yt5uhgfshfgh",
                   RoleId = 3
               },
               new Employees
               {
                   Id = 3,
                   ShortName = "I.G.",
                   FullName = EmployeeRole.Manager.ToString(),
                   Email = "i.dmitriev@test.ru",
                   Password = "retwrjuiytjghf",
                   RoleId = 2
               },
            });
        }
    }
}
