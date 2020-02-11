using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.ViewModels;

namespace TMS.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IManualDataContext db;
        public StaffController(IManualDataContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Employees
                                .Include(r => r.Role)
                                .ToListAsync());
        }
        public async Task<IActionResult> Detailed(int? id)
        {
            if (id != null)
            {
                return View(await db.Employees.Where(t => t.Id == id).Include(r => r.Role).FirstOrDefaultAsync());
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var result = await db.Employees.Where(t => t.Id == id).Include(r => r.Role).FirstOrDefaultAsync();
                var model = new StaffEditModelHybrid
                {
                     StaffId = result.Id,
                     ShortName = result.ShortName,
                     FullName = result.FullName,
                     Email = result.Email,
                     Role = (int)result.Role.Name,
                     RoleList = EmployeeEnum.RoleList((int)result.Role.Name)
                };
                return View(model);
            }
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            var model = new StaffAddModelHybrid { RoleList = EmployeeEnum.RoleList() };
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(StaffAddModelHybrid model)
        {
            if(ModelState.IsValid)
            {
                db.Employees.Add(new Employees
                {
                    ShortName = model.ShortName,
                    FullName = model.FullName,
                    Email = model.Email,
                    Role = db.Roles.FirstOrDefault(r => r.Id == model.Role)
                });
                db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(StaffEditModelHybrid model)
        {
            if (ModelState.IsValid)
            {
                var employye = db.Employees.FirstOrDefault(e => e.Id == model.StaffId);
                employye.ShortName = model.ShortName;
                employye.FullName = model.FullName;
                employye.Email = model.Email;
                employye.Role = db.Roles.FirstOrDefault(r => r.Name == (EmployeeRole)model.Role);
                db.SaveChangesAsync();
                return Redirect(Url.Action("Detailed", "Staff", model.StaffId));
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int? employyeId)
        {
            if (employyeId != null)
            {
                var employye = db.Employees.FirstOrDefault(e => e.Id == employyeId);
                db.Employees.Remove(employye);
                db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}