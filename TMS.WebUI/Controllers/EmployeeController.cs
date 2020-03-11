using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Employees.Commands.CreateEmployee;
using TMS.Application.Employees.Commands.DeleteEmployee;
using TMS.Application.Employees.Commands.UpdateEmployee;
using TMS.Application.Employees.Queries.GetEmployeeDetail;
using TMS.Application.Employees.Queries.GetEmployeeList;
using TMS.Application.Identity.Commands.AssignAppUserRole;
using TMS.Application.Identity.Commands.CreateAppUser;

namespace TMS.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetEmployeeListQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeDetailVm>> Get(long id)
        {
            return Ok(await Mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id }));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var appUserId = await Mediator.Send(new CreateAppUserCommand { Email = command.Email, Password = command.Password });

            if (!string.IsNullOrEmpty(appUserId))
            {
                await Mediator.Send(new AssignAppUserRoleCommand { AppUserId = appUserId, Role = command.Role });

                command.AppUserId = appUserId;

                var result = await Mediator.Send(command);

                if (result > 0)
                {
                    return Ok(result);
                }
            }
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update(long id, UpdateEmployeeCommand command)
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
            await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            
            return NoContent();
        }
    }
}
