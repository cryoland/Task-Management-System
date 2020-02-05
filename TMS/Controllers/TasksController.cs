using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using TMS.Models;
using TMS.ViewModels;

namespace TMS.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly TMSContext db;
        public TasksController(TMSContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var tasks = db.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter);

            IQueryable<QTask> result = tasks;

            if (!User.IsInRole(EmployeeRole.Admin.ToString()))
            {
                result = tasks.Where(u => u.Reporter.FullName.Equals(User.Identity.Name) || u.Assignee.FullName.Equals(User.Identity.Name));
            }
            return View(await result.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detailed(int? id)
        {
            if(id != null)
            {
                var tasks = db.QTasks.Where(t => t.Id == id);
                IQueryable<QTask> result = tasks;

                // админу доступны любые задачи
                if (User.IsInRole(EmployeeRole.Admin.ToString()))
                {
                    return View(await tasks.FirstOrDefaultAsync());
                }

                // фильтр: создатель или исполнитель 
                result = tasks.Where(u => u.Reporter.FullName.Equals(User.Identity.Name) || u.Assignee.FullName.Equals(User.Identity.Name));
                                             
                if (result.Count() > 0)
                {
                    return View(await result.FirstOrDefaultAsync());                   
                }
                else
                {
                    return new ContentResult { StatusCode = 403, Content = "Forbidden", ContentType = "text/html" };
                }
            }            
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Add()
        {
            return View(await db.Employees.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var task = await db.QTasks.Where(t => t.Id == id).FirstOrDefaultAsync();
                var employees = db.Employees;

                ViewBag.Task = task;
                ViewBag.IsReporter = employees.Where(u => u.FullName.Equals(User.Identity.Name)).FirstOrDefault().Id.Equals(task.ReporterId);
                ViewBag.IsAssignee = employees.Where(u => u.FullName.Equals(User.Identity.Name)).FirstOrDefault().Id.Equals(task.AssigneeId);
                ViewBag.IsAdmin = User.IsInRole(EmployeeRole.Admin.ToString());

                var employeesList = await employees.ToListAsync();
                ViewBag.Assignees = new SelectList(employeesList, "Id", "FullName", employeesList.Where(e => e.Id.Equals(task.AssigneeId)).FirstOrDefault());
                ViewBag.Reporters = new SelectList(employeesList, "Id", "FullName", employeesList.Where(e => e.Id.Equals(task.ReporterId)).FirstOrDefault());

                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Add(string title, string desc, int assigneeId, int reporterId, int priority)
        {
            var task = new QTask
            {
                Name = title,
                Description = desc,
                AssigneeId = assigneeId,
                ReporterId = reporterId,
                Priority = (TaskPriority)priority
            };
            db.QTasks.Add(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(TaskEditModel model)
        {
            if(ModelState.IsValid)
            {
                var qtask = db.QTasks.FirstOrDefault(t => t.Id == model.TaskId);
                qtask.Name = string.IsNullOrEmpty(model.Title) ? qtask.Name : model.Title;
                qtask.Description = string.IsNullOrEmpty(model.Description) ? qtask.Name : model.Description;
                qtask.AssigneeId = model.AssigneeId ?? qtask.AssigneeId;
                qtask.ReporterId = model.ReporterId ?? qtask.ReporterId;
                qtask.Priority = (TaskPriority)(model.Priority ?? (int)qtask.Priority);
                qtask.Status = (Models.TaskStatus)(model.Status ?? (int)qtask.Status);
                db.SaveChanges();
                return Redirect(Url.Action("Detailed", "Tasks", model.TaskId));
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
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}