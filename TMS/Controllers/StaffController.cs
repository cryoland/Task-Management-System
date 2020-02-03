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
    public class StaffController : Controller
    {
        private readonly TMSContext db;
        public StaffController(TMSContext context)
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                return View(await db.Employees.Where(t => t.Id == id).Include(r => r.Role).FirstOrDefaultAsync());
            }
            return RedirectToAction("Index");
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string shortname, string fullname, int role)
        {
            Role _role = db.Roles.FirstOrDefault(r => r.Id == role);
            db.Employees.Add(new Employees
            {
                ShortName = shortname,
                FullName = fullname,
                Role = _role
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(int? employyeId, string shortname, string fullname, int role)
        {
            if (employyeId != null)
            {
                Role _role = db.Roles.FirstOrDefault(r => r.Id == role);
                var employye = db.Employees.FirstOrDefault(e => e.Id == employyeId);
                employye.ShortName = shortname;
                employye.FullName = fullname;
                employye.Role = _role;
                db.SaveChanges();
                return Redirect(Url.Action("Detailed", "Staff", employyeId));
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int? employyeId)
        {
            if (employyeId != null)
            {
                var employye = db.Employees.FirstOrDefault(e => e.Id == employyeId);
                db.Employees.Remove(employye);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}