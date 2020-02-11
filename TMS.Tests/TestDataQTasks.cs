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
               new QTask { Id = 1, Name = "Name1", Description = "Description1", Priority = TaskPriority.Default,
                        Assignee = new Employees { FullName = EmployeeRole.Manager.ToString() }, 
                        AssigneeId = 1,
                        Reporter = new Employees { FullName = EmployeeRole.Admin.ToString() },
                        ReporterId = 3, Status = QTaskStatus.New },
               new QTask { Id = 2, Name = "Name2", Description = "Description2", Priority = TaskPriority.Major,
                        Assignee = new Employees { FullName = EmployeeRole.Admin.ToString() },
                        AssigneeId = 2,
                        Reporter = new Employees { FullName = EmployeeRole.Developer.ToString() },
                        ReporterId = 2, Status = QTaskStatus.New },
               new QTask { Id = 3, Name = "Name3", Description = "Description3", Priority = TaskPriority.Default,
                        Assignee = new Employees { FullName = EmployeeRole.Developer.ToString() },
                        AssigneeId = 3,
                        Reporter = new Employees { FullName = EmployeeRole.Manager.ToString() },
                        ReporterId = 1, Status = QTaskStatus.New },
            });
        }
    }
}
