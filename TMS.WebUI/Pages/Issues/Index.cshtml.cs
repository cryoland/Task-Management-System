using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Application.Issues.Queries.GetIssueList;
using TMS.Application.Employees.Queries.GetEmployeeList;
using TMS.Application.Employees.Queries.GetEmployeeDetail;

namespace TMS.WebUI
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private IssueListVm _issueVm;

        public IndexModel(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public IEnumerable<IssueDto> AssignedIssues { get; private set; }

        public IEnumerable<IssueDto> ReportedIssues { get; private set; }

        public IEnumerable<IssueDto> OtherIssues { get; private set; }

        public async Task OnGetAsync()
        {
            _issueVm = await _mediator.Send(new GetIssueListQuery());

            var currentEmployee = await _mediator.Send(new GetEmployeeByAppUserIdQuery { AppUserId = _currentUserService.UserId });
            
            AssignedIssues = _issueVm.Issues.Where(i => i.AssigneeId.Value == currentEmployee.Id);

            ReportedIssues = _issueVm.Issues.Where(i => i.ReporterId.Value == currentEmployee.Id);

            OtherIssues = _issueVm.Issues.Except(AssignedIssues).Except(ReportedIssues);
        }
    }
}
