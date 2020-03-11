using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Issues.Commands.CreateIssue;
using TMS.Application.Issues.Commands.DeleteIssue;
using TMS.Application.Issues.Commands.UpdateIssue;
using TMS.Application.Issues.Queries.GetIssueDetail;
using TMS.Application.Issues.Queries.GetIssueList;

namespace TMS.WebUI.Controllers
{
    [Authorize]
    public class IssueController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IssueListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetIssueListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IssueDetailVm>> Get(long id)
        {
            return Ok(await Mediator.Send(new GetIssueDetailQuery { IssueId = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateIssueCommand command)
        {
            var result = await Mediator.Send(command);

            if (result > 0)
            {
                return Ok(result);
            }

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(long id, UpdateIssueCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteIssueCommand { Id = id });

            return NoContent();
        }
    }
}
