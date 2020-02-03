using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Models;

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
            return View(await db.QTasks
                .Include(a => a.Assignee)
                .Include(r => r.Reporter)
                .ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detailed(int? id)
        {
            if(id != null)
            {
                return View(await db.QTasks.Where(t => t.Id == id).FirstOrDefaultAsync());
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
                return View((await db.QTasks.Where(t => t.Id == id).FirstOrDefaultAsync(), await db.Employees.ToListAsync()));
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
        public IActionResult Edit(int? taskId, string title, string desc, int assigneeId, int reporterId, int priority)
        {
            if (taskId != null)
            {
                var qtask = db.QTasks.FirstOrDefault(t => t.Id == taskId);
                qtask.Name = title;
                qtask.Description = desc;
                qtask.AssigneeId = assigneeId;
                qtask.ReporterId = reporterId;
                qtask.Priority = (TaskPriority)priority;
                db.SaveChanges();
                return Redirect(Url.Action("Detailed", "Tasks", taskId));
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