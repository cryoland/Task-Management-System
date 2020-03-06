using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMS.Application.Employees.Queries.GetEmployeeDetail;

namespace TMS.WebUI.Employees
{
    [Authorize(Roles = "Admin")]
    public class DetailedModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailedModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public EmployeeDetailVm Employee { get; private set; }

        public async Task OnGetAsync(long id)
        {
            Employee = await _mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id });
        }
    }
}
