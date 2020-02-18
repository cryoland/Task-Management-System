using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMS.Models;
using TMS.Services;
using TMS.ViewModels;
using AutoMapper;

namespace TMS.Controllers
{
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IRepositoryHandler<Employees> repositoryHandler;
        private readonly IMapper mapper;

        public StaffController(IRepositoryHandler<Employees> handler)
        {
            repositoryHandler = handler;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StaffAddModelHybrid, Employees>()
                    .ForMember(to => to.Id, from => from.Ignore())
                    .ForMember(to => to.ShortName, from => from.MapFrom(m => m.ShortName))
                    .ForMember(to => to.FullName, from => from.MapFrom(m => m.FullName))
                    .ForMember(to => to.Email, from => from.MapFrom(m => m.Email))
                    .ForMember(to => to.Role, from => from.Ignore())
                    .ForMember(to => to.RoleId, from => from.Ignore())
                    .ForMember(to => to.Password, from => from.Ignore())
                    .ForMember(to => to.AssignedQTasks, from => from.Ignore())
                    .ForMember(to => to.ReporteredQTasks, from => from.Ignore());

                cfg.CreateMap<StaffEditModelHybrid, Employees>()
                    .ForMember(to => to.Id, from => from.MapFrom(m => m.StaffId))
                    .ForMember(to => to.ShortName, from => from.MapFrom(m => m.ShortName))
                    .ForMember(to => to.FullName, from => from.MapFrom(m => m.FullName))
                    .ForMember(to => to.Email, from => from.MapFrom(m => m.Email))
                    .ForMember(to => to.Role, from => from.Ignore())
                    .ForMember(to => to.RoleId, from => from.Ignore())
                    .ForMember(to => to.Password, from => from.Ignore())
                    .ForMember(to => to.AssignedQTasks, from => from.Ignore())
                    .ForMember(to => to.ReporteredQTasks, from => from.Ignore());
            });

            // TODO: remove following statement if not developer mode
            configuration.AssertConfigurationIsValid();
            mapper = configuration.CreateMapper();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await repositoryHandler.GetAllEntriesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Detailed(int? id)
        {
            if (id != null)
            {
                return View(await repositoryHandler.GetEntryByIDAsync(id.Value));
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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
        [HttpGet]
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
                var employee = mapper.Map<Employees>(model);
                employee.Role = await repoRole.GetFirstEntityAsync(r => r.Name == (EmployeeRole)model.Role);
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
                var employee = mapper.Map<Employees>(model);
                employee.Role = await repoRole.GetFirstEntityAsync(r => r.Name == (EmployeeRole)model.Role);
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
                repositoryHandler.Delete(new Employees { Id = employyeId.Value });
            }
            return RedirectToAction("Index");
        }
    }
}