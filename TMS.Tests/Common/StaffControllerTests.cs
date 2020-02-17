using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMS.Controllers;
using TMS.Models;
using TMS.Tests.Helpers;
using TMS.Tests.Helpers.TestClasses;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using TMS.ViewModels;
using TMS.ViewModels.Tasks;
using TMS.Services;

namespace TMS.Tests.Common
{
    public class StaffControllerTests
    {
        /*
        [Fact]
        public async void ShowListOfEmployees()
        {
            var employeeDbSet = (await TestData.EmployeeGetListAsync()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<ITMSRepository>();
            dbContext.SetupGet(x => x.Employees).Returns(employeeDbSet.Object);

            var controller = new StaffController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(EmployeeRole.Developer).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            IActionResult result = await controller.Index();
            
            Assert.IsType<ViewResult>(result);
            Assert.IsType<List<Employees>>((result as ViewResult)?.ViewData?.Model);
            Assert.NotNull((result as ViewResult)?.ViewData?.Model as List<Employees>);
            Assert.True(((result as ViewResult)?.ViewData?.Model as List<Employees>).Count > 0);
        }

        [Fact]
        public async void ShowEmployeeDetailed()
        {
            var employeeDbSet = (await TestData.EmployeeGetListAsync()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<ITMSRepository>();
            dbContext.SetupGet(x => x.Employees).Returns(employeeDbSet.Object);

            var controller = new StaffController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(EmployeeRole.Developer).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            IActionResult result = await controller.Detailed(1);

            Assert.IsType<ViewResult>(result);
            Assert.IsType<Employees>((result as ViewResult)?.ViewData?.Model);
            Assert.NotNull((result as ViewResult)?.ViewData?.Model as Employees);
        }

        [Fact]
        public async void AttemptToShowNonExistedEmployee()
        {
            var employeeDbSet = (await TestData.EmployeeGetListAsync()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext.
            var dbContext = new Mock<ITMSRepository>();
            dbContext.SetupGet(x => x.Employees).Returns(employeeDbSet.Object);

            var controller = new StaffController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(EmployeeRole.Developer).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            IActionResult result = await controller.Detailed(null);

            Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(result as ViewResult);
            Assert.Null((result as ViewResult)?.ViewData);
            Assert.Null((result as ViewResult)?.ViewData?.Model);
            Assert.Equal("Index", (result as RedirectToActionResult)?.ActionName);
        }

        [Fact]
        public async void AttemptToEditEmployye()
        {
            var id = 1;
            var role = EmployeeRole.Admin;
            var employeeDbSet = (await TestData.EmployeeGetListAsync()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<ITMSRepository>();
            dbContext.SetupGet(x => x.Employees).Returns(employeeDbSet.Object);

            var controller = new StaffController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(role).Object, new RouteData(), new ControllerActionDescriptor())),
            };

            Employees testEmployee = employeeDbSet.Object.Where(e => e.Id == id).FirstOrDefault();

            IActionResult result = await controller.Edit(id);
            StaffEditModelHybrid model = (result as ViewResult)?.ViewData?.Model as StaffEditModelHybrid;

            Assert.IsType<ViewResult>(result);
            Assert.IsType<StaffEditModelHybrid>((result as ViewResult)?.ViewData?.Model);
            Assert.NotNull(result as ViewResult);
            Assert.NotNull(model);
            Assert.Equal(testEmployee.ShortName, model.ShortName);
            Assert.Equal(testEmployee.FullName, model.FullName);
            Assert.Equal(testEmployee.Email, model.Email);
            Assert.Equal((int)testEmployee.Role.Name, model.Role);
            Assert.Equal(testEmployee.Id, model.StaffId);
        }
        */
    }
}
