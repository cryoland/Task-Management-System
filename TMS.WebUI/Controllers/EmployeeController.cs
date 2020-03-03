using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Application.Employees.Commands.CreateEmployee;
using TMS.Application.Employees.Commands.DeleteEmployee;
using TMS.Application.Employees.Commands.UpdateEmployee;
using TMS.Application.Employees.Queries.GetEmployeeDetail;
using TMS.Application.Employees.Queries.GetEmployeeList;

namespace TMS.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ContentController
    {
        private readonly IIdentityService _identityService;

        public EmployeeController(IIdentityService identityService) : base()
        {
            _identityService = identityService;
        }

        public async Task<ActionResult> Index()
        {            
            return View(await Mediator.Send(new GetEmployeeListQuery()));
        }

        public async Task<ActionResult> Detailed([FromRoute]int id)
        {
            return View(await Mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id }));
        }

        public async Task<ActionResult> Edit([FromRoute]int id)
        {
            return View(await Mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id }));
        }

        public async Task<ActionResult> Create()
        {
            return View(await Mediator.Send(new GetEmptyEmployeeQuery()));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateEmployeeCommand command)
        {
            var dict = new ModelStateDictionary();

            if (!await _identityService.IsUserExistAsync(command.Email))
            {
                var (Result, UserId) = await _identityService.CreateUserAsync(command.Email, command.Password);              

                if (Result.Succeeded)
                {
                    await _identityService.AddRoleToUserAsync(UserId, command.Role.ToString());
                    
                    command.AppUserId = UserId;

                    var result = await Mediator.Send(command);

                    if (result > 0)
                    {
                        return RedirectToAction(nameof(this.Index));
                    }
                    else
                    {
                        dict.AddModelError("ValidationErr", result.ToString());
                    }
                }
                else
                {
                    foreach(var error in Result.Errors)
                    {
                        dict.AddModelError("IdentityServiceErr", error);
                    }
                    return BadRequest(Result.Errors);
                }
            }
            else
            {
                dict.AddModelError("UserExistErr", $"User with email {command.Email} is already exist!");
            }

            if (dict.Count == 0)
            {
                dict.AddModelError("UnknErr", "Unhandled error");
            }
            return BadRequest(dict);
        }

        [HttpPost]
        public async Task<ActionResult> Update(long id, UpdateEmployeeCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await Mediator.Send(command);
            return RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            return RedirectToAction(nameof(this.Index));
        }
    }
}
