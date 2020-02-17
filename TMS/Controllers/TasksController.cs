using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Services;
using TMS.ViewModels;

namespace TMS.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IRepositoryHandler<QTask> repositoryHandler;

        public TasksController(IRepositoryHandler<QTask> handler)
        {
            repositoryHandler = handler;
        }

        public async Task<IActionResult> Index(string sortorder, [FromServices]IDataSorter<QTask> sorter)
        {
            var result = await repositoryHandler.GetAllEntriesAsync();

            var model = new TaskIndexModel
            {
                PrioritySort = sortorder == TaskSort.PriotityAsc ? TaskSort.PriotityDesc : TaskSort.PriotityAsc,
                NameSort = string.IsNullOrEmpty(sortorder) ? TaskSort.NameDesc : string.Empty,
                StatusSort = sortorder == TaskSort.StatusAsc ? TaskSort.StatusDesc : TaskSort.StatusAsc,
                AssigneeTaskList = await repositoryHandler.GetAllEntriesAsync(a => a.Assignee.FullName.Equals(User.Identity.Name)),
                ReporterTaskList = await repositoryHandler.GetAllEntriesAsync(r => r.Reporter.FullName.Equals(User.Identity.Name)),
            };

            if (User.IsInRole(EmployeeRole.Admin.ToString()))
            {
                model.OtherTaskList = result.Except(model.AssigneeTaskList).Except(model.ReporterTaskList);
            }

            // TODO: adapt sorter
            // IQueryable<QTask> result = sorter.Sort(tasks, sortorder);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detailed(int? id)
        {
            if (id != null)
            {
                var task = await repositoryHandler.GetEntryByIDAsync(id.Value, u => u.Reporter.FullName.Equals(User.Identity.Name) ||
                                    u.Assignee.FullName.Equals(User.Identity.Name) ||
                                    User.IsInRole(EmployeeRole.Admin.ToString()));

                if (task is null)
                {
                    return new ContentResult { StatusCode = 403, Content = "Forbidden", ContentType = "text/html" };
                }

                return View(task);
            }
            return RedirectToAction("Index");
        }

        // TODO: implement EmployeeHandler        
        public async Task<IActionResult> Add([FromServices]IRepositoryHandler<Employees> repoEmployee)
        {
            ViewBag.IsAdmin = User.IsInRole(EmployeeRole.Admin.ToString());
            var employeesList = new SelectList(await repoEmployee.GetAllEntriesAsync(), "Id", "FullName");
            var model = new TaskAddModelHybrid
            {
                ReporterId = (await repoEmployee.GetFirstEntityAsync(u => u.FullName.Equals(User.Identity.Name))).Id,            
                AssigneeList = employeesList,
                ReporterList = employeesList,
                PriorityList = TaskEnum.PriorityList()
            };                       
            return View(model);
        }

        // TODO: implement EmployeeHandler   
        [HttpGet]
        public async Task<IActionResult> Edit(int? id, [FromServices]IRepositoryHandler<Employees> repoEmployee)
        {
            if (id != null)
            {
                var task = await repositoryHandler.GetEntryByIDAsync(id.Value, u => u.Reporter.FullName.Equals(User.Identity.Name) ||
                                    u.Assignee.FullName.Equals(User.Identity.Name) ||
                                    User.IsInRole(EmployeeRole.Admin.ToString()));

                if (task is null)
                {
                    return new ContentResult { StatusCode = 403, Content = "Forbidden", ContentType = "text/html" };
                }

                var employeesList = await repoEmployee.GetAllEntriesAsync();

                ViewBag.IsAdmin = User.IsInRole(EmployeeRole.Admin.ToString());
                ViewBag.IsReporter = (await repoEmployee.GetFirstEntityAsync(u => u.FullName.Equals(User.Identity.Name))).Id.Equals(task.ReporterId);

                var model = new TaskEditModelHybrid
                {
                    Task = task,
                    AssigneeList = new SelectList(employeesList, "Id", "FullName", task.AssigneeId),
                    ReporterList = new SelectList(employeesList, "Id", "FullName", task.ReporterId),
                    PriorityList = TaskEnum.PriorityList((int)task.Priority),
                    StatusList = TaskEnum.StatusList((int)task.Status),
                };

                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(TaskAddModelHybrid model)
        {
            if(ModelState.IsValid)
            {
                var task = new QTask
                {
                    Name = model.Title,
                    Description = model.Description,
                    AssigneeId = model.AssigneeId,
                    ReporterId = model.ReporterId,
                    Priority = (TaskPriority)model.Priority
                };
                repositoryHandler.Create(task);
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Edit(TaskEditModelHybrid model)
        {
            if(ModelState.IsValid)
            {
                var task = new QTask
                {
                    Id = model.TaskId,
                    Name = model.Title,
                    Description = model.Description,
                    AssigneeId = model.AssigneeId,
                    ReporterId = model.ReporterId,
                    Priority = (TaskPriority)model.Priority,
                    Status = (QTaskStatus)model.Status                    
                };
                repositoryHandler.Update(task);
                return RedirectToAction("Detailed", "Tasks", model.TaskId);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? taskId)
        {
            if (taskId != null)
            {
                var task = new QTask { Id = taskId.Value };
                repositoryHandler.Delete(task);
            }
            return RedirectToAction("Index");
        }
    }
}