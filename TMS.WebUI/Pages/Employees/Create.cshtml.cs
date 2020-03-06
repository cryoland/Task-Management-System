using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMS.Application.Employees.Commands.CreateEmployee;
using TMS.Application.Employees.Queries.GetEmployeeDetail;
using TMS.Application.Identity.Commands.AssignAppUserRole;
using TMS.Application.Identity.Commands.CreateAppUser;

namespace TMS.WebUI.Employees
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public EmployeeDetailVm Employee { get; private set; }

        public async Task OnGetAsync()
        {
            Employee = await _mediator.Send(new GetEmptyEmployeeQuery());
        }

        public async Task<IActionResult> OnPostAsync(CreateEmployeeCommand command)
        {
            var appUserId = await _mediator.Send(new CreateAppUserCommand { Email = command.Email, Password = command.Password });

            if (!string.IsNullOrEmpty(appUserId))
            {
                await _mediator.Send(new AssignAppUserRoleCommand { AppUserId = appUserId, Role = command.Role });

                command.AppUserId = appUserId;

                var result = await _mediator.Send(command);

                if (result > 0)
                {
                    return RedirectToPage("./Detailed", new { id = result });
                }
            }            
            return BadRequest();
        }
    }
}
