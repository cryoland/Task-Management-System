using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Services;
using TMS.ViewModels;

namespace TMS.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IRepositoryHandler<Employees> repositoryHandler;
        public StaffController(IRepositoryHandler<Employees> handler)
        {
            repositoryHandler = handler;
        }
        public async Task<IActionResult> Index()
        {
            return View(await repositoryHandler.GetAllEntriesAsync());
        }
        public async Task<IActionResult> Detailed(int? id)
        {
            if (id != null)
            {
                return View(await repositoryHandler.GetEntryByIDAsync(id.Value));
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var result = await repositoryHandler.GetEntryByIDAsync(id.Value);
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
        public async Task<IActionResult> Add(StaffAddModelHybrid model, [FromServices]IRepositoryHandler<Role> repoRole)
        {
            if(ModelState.IsValid)
            {
                var employee = new Employees
                {
                    ShortName = model.ShortName,
                    FullName = model.FullName,
                    Email = model.Email,
                    Role = await repoRole.GetFirstEntityAsync(r => r.Name == (EmployeeRole)model.Role),
                };
                repositoryHandler.Create(employee);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(StaffEditModelHybrid model, [FromServices]IRepositoryHandler<Role> repoRole)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employees
                {
                    Id = model.StaffId,
                    ShortName = model.ShortName,
                    FullName = model.FullName,
                    Email = model.Email,
                    Role = await repoRole.GetFirstEntityAsync(r => r.Name == (EmployeeRole)model.Role),
                };
                repositoryHandler.Update(employee);
                return RedirectToAction("Detailed", "Staff", model.StaffId);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int? employyeId)
        {
            if (employyeId != null)
            {
                var employee = new Employees { Id = employyeId.Value };
                repositoryHandler.Delete(employee);
            }
            return RedirectToAction("Index");
        }
    }
}