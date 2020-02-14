using System;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleTMS
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContextOptionsBuilder<TMSRepository> optionsBuilder = new DbContextOptionsBuilder<TMSRepository>();
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=TMSdb; Trusted_Connection=True;");
            using var context = new TMSRepository(optionsBuilder.Options);

            var r1 = new Role { Name = EmployeeRole.Admin };
            var r2 = new Role { Name = EmployeeRole.Manager };
            var r3 = new Role { Name = EmployeeRole.Developer };

            context.Roles.AddRange(new List<Role>() { r1, r2, r3 });
            context.SaveAsync();

            var e1 = new Employees { ShortName = "D.V.", FullName = "Dmitry Dmitrievich Vasilev", Role = r1, Email = "d.vasilev@test.ru", Password = "d.vasilev@test.ru" };
            var e2 = new Employees { ShortName = "E.G.", FullName = "Evgeny Alekseevich Grigoriev", Role = r2, Email = "e.grigoriev@test.ru", Password = "e.grigoriev@test.ru" };
            context.Employees.AddRange(new List<Employees> { e1, e2 });
            context.SaveAsync(); // must have before cause of IDs must be rewritten for prevent its duplication

            var t1 = new QTask
            {
                Name = "Task#0",
                Description = "Test task to explore features of EF Core",
                Status = QTaskStatus.New,
                Priority = TaskPriority.Default,
                Assignee = e2,
                Reporter = e1,
            };
            var t2 = new QTask
            {
                Name = "Task#1",
                Description = "Task to solve major problems in project",
                Status = QTaskStatus.New,
                Priority = TaskPriority.Major,
                Assignee = e2,
                Reporter = e1,
            };
            context.QTasks.AddRange(new List<QTask> { t1, t2 });

            context.SaveAsync();
            args = null;
        }
    }
}
