using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMS.Application.Issues.Queries.GetIssueDetail;

namespace TMS.WebUI.Issues
{
    [Authorize]
    public class DetailedModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailedModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IssueDetailVm Issue { get; private set; }

        public async Task OnGetAsync(long id)
        {
            Issue = await _mediator.Send(new GetIssueDetailQuery { IssueId = id });
        }
    }
}
