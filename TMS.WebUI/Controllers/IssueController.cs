using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Issues.Commands.CreateIssue;
using TMS.Application.Issues.Commands.UpdateIssue;
using TMS.Application.Issues.Commands.DeleteIssue;
using TMS.Application.Issues.Queries.GetIssueDetail;
using TMS.Application.Issues.Queries.GetIssueList;
using Microsoft.AspNetCore.Authorization;

namespace TMS.WebUI.Controllers
{
    [Authorize]
    public class IssueController : ContentController
    {
        public async Task<ActionResult> Index()
        {
            return View(await Mediator.Send(new GetIssueListQuery()));
        }

        public async Task<ActionResult> Detailed([FromRoute]int id)
        {
            return View(await Mediator.Send(new GetIssueDetailQuery { IssueId = id }));
        }

        public async Task<ActionResult> Edit([FromRoute]int id)
        {
            return View(await Mediator.Send(new GetIssueDetailForEditQuery { IssueId = id }));
        }

        public async Task<ActionResult> Create()
        {
            return View(await Mediator.Send(new GetEmptyIssueQuery()));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateIssueCommand command)
        {
            var result = await Mediator.Send(command);
            if (result > 0)
            {
                return LocalRedirect("~/Issues");
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> Update(long id, UpdateIssueCommand command)
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
            await Mediator.Send(new DeleteIssueCommand { Id = id });
            return RedirectToAction(nameof(this.Index));
        }
    }
}
