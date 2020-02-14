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

        // TODO: remake methods
        /*
        [HttpGet]
        public async Task<IActionResult> Detailed(int? id)
        {
            if (id != null)
            {
                var task = db.QTasks.Where(t => t.Id == id);
                var result = task.Where(
                                    u => u.Reporter.FullName.Equals(User.Identity.Name) ||
                                    u.Assignee.FullName.Equals(User.Identity.Name) ||
                                    User.IsInRole(EmployeeRole.Admin.ToString()));

                if (result.Count() == 0)
                {
                    return new ContentResult { StatusCode = 403, Content = "Forbidden", ContentType = "text/html" };
                }

                return View(await result.FirstOrDefaultAsync());
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.IsAdmin = User.IsInRole(EmployeeRole.Admin.ToString());
            var employeesList = new SelectList(await db.Employees.ToListAsync(), "Id", "FullName");
            var model = new TaskAddModelHybrid
            {
                ReporterId = db.Employees.Where(u => u.FullName.Equals(User.Identity.Name)).FirstOrDefault().Id,                
                AssigneeList = employeesList,
                ReporterList = employeesList,
                PriorityList = TaskEnum.PriorityList()
            };                       
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var result = db.QTasks.Where(t => t.Id == id)
                                      .Where(
                                        u => u.Reporter.FullName.Equals(User.Identity.Name) || 
                                        u.Assignee.FullName.Equals(User.Identity.Name) || 
                                        User.IsInRole(EmployeeRole.Admin.ToString()));
                if (result.Count() == 0)
                {
                    return new ContentResult { StatusCode = 403, Content = "Forbidden", ContentType = "text/html" };
                }
                var task = await result.FirstOrDefaultAsync();
                var employees = db.Employees;
                var employeesList = await employees.ToListAsync();

                ViewBag.IsAdmin = User.IsInRole(EmployeeRole.Admin.ToString());
                ViewBag.IsReporter = employees.Where(u => u.FullName.Equals(User.Identity.Name)).FirstOrDefault().Id.Equals(task.ReporterId);

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
                db.QTasks.Add(task);
                db.SaveAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(TaskEditModelHybrid model)
        {
            if(ModelState.IsValid)
            {
                var qtask = db.QTasks.FirstOrDefault(t => t.Id == model.TaskId);
                qtask.Name = string.IsNullOrEmpty(model.Title) ? qtask.Name : model.Title;
                qtask.Description = string.IsNullOrEmpty(model.Description) ? qtask.Name : model.Description;
                qtask.AssigneeId = model.AssigneeId ?? qtask.AssigneeId;
                qtask.ReporterId = model.ReporterId ?? qtask.ReporterId;
                qtask.Priority = (TaskPriority)(model.Priority ?? (int)qtask.Priority);
                qtask.Status = (QTaskStatus)(model.Status ?? (int)qtask.Status);
                db.SaveAsync();
                return RedirectToAction("Detailed", "Tasks", model.TaskId);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? taskId)
        {
            if (taskId != null)
            {
                var qtask = db.QTasks.FirstOrDefault(t => t.Id == taskId);
                db.QTasks.Remove(qtask);
                db.SaveAsync();
            }
            return RedirectToAction("Index");
        }
        */
    }
}