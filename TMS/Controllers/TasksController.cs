using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Services;
using TMS.ViewModels.Tasks;
using AutoMapper;

namespace TMS.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly IRepositoryHandler<QTask> repositoryHandler;
        private readonly IMapper mapper;

        public TasksController(IRepositoryHandler<QTask> handler)
        {
            repositoryHandler = handler;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskAddModelHybrid, QTask>()
                   .ForMember(to => to.Priority, from => from.MapFrom(m => (TaskPriority)m.Priority))
                   .ForMember(to => to.Name, from => from.MapFrom(m => m.Title))
                   .ForMember(to => to.Assignee, from => from.Ignore())
                   .ForMember(to => to.Reporter, from => from.Ignore())
                   .ForMember(to => to.Status, from => from.Ignore())
                   .ForMember(to => to.Id, from => from.Ignore());
                cfg.CreateMap<TaskEditModelHybrid, QTask>()
                   .ForMember(to => to.Priority, from => from.MapFrom(m => m.Priority.HasValue ? (TaskPriority)m.Priority : TaskPriority.None))
                   .ForMember(to => to.Status, from => from.MapFrom(m => m.Status.HasValue ? (QTaskStatus)m.Status : QTaskStatus.None))
                   .ForMember(to => to.Name, from => from.MapFrom(m => m.Title))
                   .ForMember(to => to.Id, from => from.MapFrom(m => m.TaskId))
                   .ForMember(to => to.Assignee, from => from.Ignore())
                   .ForMember(to => to.Reporter, from => from.Ignore());
            });
            configuration.AssertConfigurationIsValid();            
            mapper = configuration.CreateMapper();
        }

        public async Task<IActionResult> Index(string sortorder, [FromServices]IDataSorter<QTask> sorter)
        {
            var result = await repositoryHandler.GetAllEntriesAsync();

            IEnumerable<QTask> assigneeTasks = await repositoryHandler.GetAllEntriesAsync(a => a.Assignee.FullName.Equals(User.Identity.Name));
            IEnumerable<QTask> reporterTasks = await repositoryHandler.GetAllEntriesAsync(r => r.Reporter.FullName.Equals(User.Identity.Name));
            IEnumerable<QTask> otherTasks = result.Except(assigneeTasks).Except(reporterTasks);

            if(!string.IsNullOrEmpty(sortorder))
            {
                assigneeTasks = sorter.Sort(assigneeTasks, sortorder);
                reporterTasks = sorter.Sort(reporterTasks, sortorder);
                otherTasks = sorter.Sort(otherTasks, sortorder);
            }

            var model = new TaskIndexModel
            {
                PrioritySort = sortorder == TaskSort.PriotityAsc ? TaskSort.PriotityDesc : TaskSort.PriotityAsc,
                NameSort = sortorder == TaskSort.NameDesc ? string.Empty : TaskSort.NameDesc,
                StatusSort = sortorder == TaskSort.StatusAsc ? TaskSort.StatusDesc : TaskSort.StatusAsc,
                AssigneeTaskList = assigneeTasks,
                ReporterTaskList = reporterTasks,
            };

            if (User.IsInRole(EmployeeRole.Admin.ToString()))
            {
                model.OtherTaskList = otherTasks;
            }

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
                var task = mapper.Map<QTask>(model);          
                repositoryHandler.Create(task);
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Edit(TaskEditModelHybrid model)
        {
            if(ModelState.IsValid)
            {
                var task = mapper.Map<QTask>(model);
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
                repositoryHandler.Delete(new QTask { Id = taskId.Value });
            }
            return RedirectToAction("Index");
        }
    }
}