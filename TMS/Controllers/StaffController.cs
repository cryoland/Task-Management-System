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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                return View(await db.Employees.Where(t => t.Id == id).Include(r => r.Role).FirstOrDefaultAsync());
            }
            return RedirectToAction("Index");
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Add(string shortname, string fullname, string email, int role)
        {
            Role _role = db.Roles.FirstOrDefault(r => r.Id == role);
            db.Employees.Add(new Employees
            {
                ShortName = shortname,
                FullName = fullname,
                Email = email,
                Role = _role
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(int? employyeId, string shortname, string fullname, string email, int role)
        {
            if (employyeId != null)
            {
                Role _role = db.Roles.FirstOrDefault(r => r.Id == role);
                var employye = db.Employees.FirstOrDefault(e => e.Id == employyeId);
                employye.ShortName = shortname;
                employye.FullName = fullname;
                employye.Email = email;
                employye.Role = _role;
                db.SaveChanges();
                return Redirect(Url.Action("Detailed", "Staff", employyeId));
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
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}