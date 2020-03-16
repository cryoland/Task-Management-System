using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;
using TMS.Persistence;

namespace TMS.UnitTests.Application.Common
{
    public class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

            var operationalStoreOptions = Options.Create(
                new OperationalStoreOptions
                {
                    DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
                    PersistedGrants = new TableConfiguration("PersistedGrants")
                });

            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now)
                    .Returns(new DateTime(3001, 1, 1));

            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(m => m.UserId)
                    .Returns("00000000-0000-0000-0000-000000000000");

            var context = new ApplicationDbContext(
                options, operationalStoreOptions,
                currentUserServiceMock.Object, dateTimeMock.Object);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(ApplicationDbContext context)
        {
            context.Employees.AddRange(
                new Employee { EmployeeId = 1, FullName = "FIO_1", ShortName = "sn1", AppUserId = Guid.NewGuid().ToString() },
                new Employee { EmployeeId = 2, FullName = "FIO_2", ShortName = "sn2", AppUserId = Guid.NewGuid().ToString() },
                new Employee { EmployeeId = 3, FullName = "FIO_3", ShortName = "sn3", AppUserId = Guid.NewGuid().ToString() }
            );

            context.Issues.AddRange(
                new Issue { IssueId = 1, Name = "Name_1", Description = "Description_1", AssigneeId = 2, ReporterId = 1, Status = IssueStatus.New },
                new Issue { IssueId = 2, Name = "Name_2", Description = "Description_2", AssigneeId = 3, ReporterId = 1, Status = IssueStatus.New },
                new Issue { IssueId = 3, Name = "Name_3", Description = "Description_3", AssigneeId = 1, ReporterId = 2, Status = IssueStatus.New },
                new Issue { IssueId = 4, Name = "Name_4", Description = "Description_4", AssigneeId = 3, ReporterId = 2, Status = IssueStatus.New },
                new Issue { IssueId = 5, Name = "Name_5", Description = "Description_5", AssigneeId = 2, ReporterId = 3, Status = IssueStatus.New }
            );

            context.SaveChanges();
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
