using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Employees.Commands.CreateEmployee;

namespace TMS.WebUI.Controllers.Api
{
    public class EmployeeApiController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateEmployeeCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}