using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TMS.Application.Issues.Commands.DeleteIssue;
using TMS.Application.Issues.Commands.UpdateIssue;
using TMS.Application.Issues.Queries.GetIssueDetail;

namespace TMS.WebUI.Issues
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IssueDetailForUpsertVm Issue { get; private set; }

        public async Task OnGetAsync(long id)
        {
            Issue = await _mediator.Send(new GetIssueDetailForEditQuery { IssueId = id });
        }

        public async Task<IActionResult> OnPostAsync(long id, UpdateIssueCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);

            return RedirectToPage("./Detailed", new { id = command.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            await _mediator.Send(new DeleteIssueCommand { Id = id });

            return RedirectToPage("./Index");
        }
    }
}
