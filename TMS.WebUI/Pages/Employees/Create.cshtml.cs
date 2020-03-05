using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMS.Application.Common.Interfaces;
using TMS.Application.Employees.Commands.CreateEmployee;
using TMS.Application.Employees.Queries.GetEmployeeDetail;

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
            var result = await _mediator.Send(command);
            if (result > 0)
            {
                return RedirectToPage("./Detailed", new { id = result });
            }
            return BadRequest(result);
        }
    }
}
