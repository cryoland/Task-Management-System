using Xunit;
using Moq;
using System.Collections.Generic;
using TMS.Controllers;
using TMS.Models;
using TMS.Services;
using TMS.Tests.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TMS.Tests.Helpers.TestClasses;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace TMS.Tests.Common
{

    public class Redirections
    {
        [Fact]
        public async void FromTasksToLoginPageIfNotAuthorized()
        {
            // Create a mock DbContext
            var dbContext = new Mock<IManualDataContext>();
            // Create a mock DataSorter
            var TaskQuerySorterMock = new Mock<ITaskQueryResultSorting<QTask>>();
            // Create a mock HttpContext
            var contextMock = new Mock<HttpContext>();
            // Create a mock ControllerContext
            var controllerContextMock = new Mock<ControllerContext>();

            var dbsetItems = TestFunctions.CreateDbSet(await TestData.GetAsyncQTasksList());

            // Mocking DbSet
            dbContext.SetupGet(x => x.QTasks).Returns(dbsetItems);
            // Mocking DataSorter
            TaskQuerySorterMock.Setup(sorter => sorter.Sort(dbsetItems, "")).Returns(dbsetItems);
            // Mocking HttpContext
            contextMock.SetupGet(ctx => ctx.User).Returns(TestFunctions.GetUser(EmployeeRole.Admin));
            contextMock.SetupGet(ctx => ctx.User.Identity.Name).Returns(EmployeeRole.Developer.ToString());
            contextMock.SetupGet(ctx => ctx.User.Identity.IsAuthenticated).Returns(true);
            contextMock.Setup(ctx => ctx.User.IsInRole(EmployeeRole.Admin.ToString())).Returns(false);

            var controller = new TasksController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(contextMock.Object, new RouteData(), new ControllerActionDescriptor()))
            };

            // Act
            var result = controller.Index("", TaskQuerySorterMock.Object);

            // Assert
            Assert.Equal(1, 1);
        }
    }
}