using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TMS.Application.Issues.Commands.CreateIssue;
using TMS.Application.Issues.Queries.GetIssueDetail;

namespace TMS.WebUI.Issues
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IssueDetailForUpsertVm Issue { get; set; }

        public async Task OnGetAsync()
        {
            Issue = await _mediator.Send(new GetEmptyIssueQuery());
        }

        public async Task<IActionResult> OnPostAsync(CreateIssueCommand command)
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
