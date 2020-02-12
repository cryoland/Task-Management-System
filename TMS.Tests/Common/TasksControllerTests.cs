using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;
using Xunit;
using Moq;
using TMS.Controllers;
using TMS.Models;
using TMS.Services;
using TMS.Tests.Helpers;
using TMS.Tests.Helpers.TestClasses;
using TMS.ViewModels;

namespace TMS.Tests.Common
{
    public class TaskControllerTests
    {
        [Fact]
        public async void TaskListForAdmin()
        {
            // Testing Role
            var role = EmployeeRole.Admin;
            var tasksDbSet = (await TestData.GetAsyncQTasksList()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<IManualDataContext>();
            dbContext.SetupGet(x => x.QTasks).Returns(tasksDbSet.Object);

            // Create a mock DataSorter
            var TaskQuerySorterMock = new Mock<ITaskQueryResultSorting<QTask>>();
            TaskQuerySorterMock.Setup(s => s.Sort(It.IsAny<IQueryable<QTask>>(), It.IsAny<string>())).Returns(tasksDbSet.Object);

            var controller = new TasksController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(role).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            // Act
            var result = controller.Index(string.Empty, TaskQuerySorterMock.Object);
            var notAdminTasks = tasksDbSet.Object.Where(t => t.Reporter.FullName != EmployeeRole.Admin.ToString() && t.Assignee.FullName != EmployeeRole.Admin.ToString()).Count();
            var assigneeTasks = ((result.Result as ViewResult).Model as TaskIndexModel).AssigneeTaskList;
            var reporterTasks = ((result.Result as ViewResult).Model as TaskIndexModel).ReporterTaskList;
            var otherTasks = ((result.Result as ViewResult).Model as TaskIndexModel).OtherTaskList;
            var prioritySort = ((result.Result as ViewResult).Model as TaskIndexModel).PrioritySort;
            var nameSort = ((result.Result as ViewResult).Model as TaskIndexModel).NameSort;
            var statusSort = ((result.Result as ViewResult).Model as TaskIndexModel).StatusSort;

            // Assert
            Assert.NotNull(assigneeTasks);
            Assert.NotNull(reporterTasks);
            Assert.NotNull(otherTasks);
            Assert.Equal(notAdminTasks, otherTasks.Count());
            Assert.Equal(tasksDbSet.Object.Count(), assigneeTasks.Count() + reporterTasks.Count() + otherTasks.Count());
            Assert.Equal("priority_asc", prioritySort);
            Assert.Equal("name_desc", nameSort);
            Assert.Equal("status_asc", statusSort);
        }

        [Fact]
        public async void TaskListForUser()
        {
            // Testing Role
            var role = EmployeeRole.Developer;
            var tasksDbSet = (await TestData.GetAsyncQTasksList()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<IManualDataContext>();
            dbContext.SetupGet(x => x.QTasks).Returns(tasksDbSet.Object);

            // Create a mock DataSorter
            var TaskQuerySorterMock = new Mock<ITaskQueryResultSorting<QTask>>();
            TaskQuerySorterMock.Setup(s => s.Sort(It.IsAny<IQueryable<QTask>>(), It.IsAny<string>())).Returns(tasksDbSet.Object);

            var controller = new TasksController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(role).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            // Act
            var result = controller.Index(string.Empty, TaskQuerySorterMock.Object);
            var alienTasks = tasksDbSet.Object.Where(t => t.Reporter.FullName != role.ToString() && t.Assignee.FullName != role.ToString());
            var assigneeTasks = ((result.Result as ViewResult).Model as TaskIndexModel).AssigneeTaskList;
            var reporterTasks = ((result.Result as ViewResult).Model as TaskIndexModel).ReporterTaskList;
            var otherTasks = ((result.Result as ViewResult).Model as TaskIndexModel).OtherTaskList;
            var prioritySort = ((result.Result as ViewResult).Model as TaskIndexModel).PrioritySort;
            var nameSort = ((result.Result as ViewResult).Model as TaskIndexModel).NameSort;
            var statusSort = ((result.Result as ViewResult).Model as TaskIndexModel).StatusSort;

            // Assert
            Assert.Null(otherTasks);
            Assert.NotNull(assigneeTasks);
            Assert.NotNull(reporterTasks);
            Assert.Equal(tasksDbSet.Object.Count() - alienTasks.Count(), assigneeTasks.Count() + reporterTasks.Count());
            Assert.Equal("priority_asc", prioritySort);
            Assert.Equal("name_desc", nameSort);
            Assert.Equal("status_asc", statusSort);
        }

        [Fact]
        public async void TaskCanBeViewedOnlyByAssigneeOrReporter()
        {
            var role = EmployeeRole.Developer;
            var tasksDbSet = (await TestData.GetAsyncQTasksList()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<IManualDataContext>();
            dbContext.SetupGet(x => x.QTasks).Returns(tasksDbSet.Object);

            var controller = new TasksController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(role).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            // Act
            var result_id10_TaskIsNotExist = controller.Detailed(10);
            var result_id1_TaskIsAlien = controller.Detailed(1);
            var result_id2_TaskIsReporter = controller.Detailed(2);
            var result_idNull = controller.Detailed(null);
            var idIsNotExist_statusCode = (result_id10_TaskIsNotExist.Result as ContentResult)?.StatusCode;
            var idIsAlien_statusCode = (result_id1_TaskIsAlien.Result as ContentResult)?.StatusCode;
            var idIsNull_statusCode = (result_idNull.Result as RedirectToActionResult).ActionName;
            var id2_ReporterTask = (result_id2_TaskIsReporter.Result as ViewResult).Model;

            // Assert
            Assert.NotNull(id2_ReporterTask);
            Assert.Equal(403, idIsNotExist_statusCode);
            Assert.Equal(403, idIsAlien_statusCode);
            Assert.Equal("Index", idIsNull_statusCode);
        }

        [Fact]
        public async void AlienTaskCanBeViewedByAdmin()
        {
            var role = EmployeeRole.Admin;
            var tasksDbSet = (await TestData.GetAsyncQTasksList()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<IManualDataContext>();
            dbContext.SetupGet(x => x.QTasks).Returns(tasksDbSet.Object);

            var controller = new TasksController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(role).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            // Act
            var result_id10_TaskIsNotExist = controller.Detailed(10);
            var result_id1_TaskIsAlien = controller.Detailed(1);
            var result_id2_TaskIsReporter = controller.Detailed(2);
            var result_idNull = controller.Detailed(null);
            var idIsNotExist_statusCode = (result_id10_TaskIsNotExist.Result as ContentResult)?.StatusCode;
            var idIsNull_statusCode = (result_idNull.Result as RedirectToActionResult).ActionName;
            var id2_ReporterTask = (result_id2_TaskIsReporter.Result as ViewResult).Model;
            var idIsAlien_Task = (result_id1_TaskIsAlien.Result as ViewResult).Model;

            // Assert
            Assert.NotNull(id2_ReporterTask);
            Assert.NotNull(idIsAlien_Task);
            Assert.Equal(403, idIsNotExist_statusCode);
            Assert.Equal("Index", idIsNull_statusCode);
        }

        [Fact]
        public async void TaskCanBeEditedOnlyByAssigneeOrReporter()
        {
            var role = EmployeeRole.Manager;
            var correctTaskId = 1;

            var tasksDbSet = (await TestData.GetAsyncQTasksList()).AsQueryable().BuildMockDbSet();
            var employeeDbSet = (await TestData.GetAsyncEmployeeList()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<IManualDataContext>();
            dbContext.SetupGet(x => x.QTasks).Returns(tasksDbSet.Object);
            dbContext.SetupGet(x => x.Employees).Returns(employeeDbSet.Object);

            var controller = new TasksController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(role).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            // Act
            var result_AdminToManagerTask = controller.Edit(correctTaskId);
            var result_DevToAdminTask = controller.Edit(2);
            var model = (result_AdminToManagerTask.Result as ViewResult).Model as TaskEditModelHybrid;
            var currentTaskId = model.Task.Id;
            var checkAssigneeListCount = model.AssigneeList.Count();
            var checkReporterListCount = model.ReporterList.Count();
            var idIsAlien_statusCode = (result_DevToAdminTask.Result as ContentResult)?.StatusCode;
            
            // Assert
            Assert.Equal(employeeDbSet.Object.Count(), checkAssigneeListCount);
            Assert.Equal(employeeDbSet.Object.Count(), checkReporterListCount);
            Assert.Equal(correctTaskId, currentTaskId);
            Assert.Equal(403, idIsAlien_statusCode);
        }

        [Fact]
        public async void AlienTaskCanBeEditedByAdmin()
        {
            var role = EmployeeRole.Admin;
            var alienTaskId = 4;

            var tasksDbSet = (await TestData.GetAsyncQTasksList()).AsQueryable().BuildMockDbSet();
            var employeeDbSet = (await TestData.GetAsyncEmployeeList()).AsQueryable().BuildMockDbSet();

            // Create a mock DbContext
            var dbContext = new Mock<IManualDataContext>();
            dbContext.SetupGet(x => x.QTasks).Returns(tasksDbSet.Object);
            dbContext.SetupGet(x => x.Employees).Returns(employeeDbSet.Object);

            var controller = new TasksController(dbContext.Object)
            {
                ControllerContext = new ControllerContext(new ActionContext(FakeHttpContextBuilder.Build(role).Object, new RouteData(), new ControllerActionDescriptor()))
            };

            var result_alienTask = controller.Edit(alienTaskId);
            var model = (result_alienTask.Result as ViewResult).Model as TaskEditModelHybrid;
            var redirect = (result_alienTask.Result as ContentResult)?.StatusCode;

            // Assert
            Assert.NotNull(model);
            Assert.Null(redirect);
            Assert.NotEqual(EmployeeRole.Admin.ToString(), model.Task.Assignee.FullName);
            Assert.NotEqual(EmployeeRole.Admin.ToString(), model.Task.Reporter.FullName);
        }
    }
}