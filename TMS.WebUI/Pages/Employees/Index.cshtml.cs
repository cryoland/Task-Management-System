using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMS.Application.Common.Interfaces;
using TMS.Application.Employees.Queries.GetEmployeeList;

namespace TMS.WebUI.Employees
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IEnumerable<EmployeeDto> Employees { get; private set; }

        public async Task OnGetAsync()
        {
            Employees = (await _mediator.Send(new GetEmployeeListQuery())).Employees;
        }
    }
}
