using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Employees.Commands.CreateEmployee;
using TMS.Application.Employees.Queries.GetEmployeeDetail;
using TMS.Application.Employees.Queries.GetEmployeeList;

namespace TMS.WebUI.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<EmployeeListVm>> Index()
        {            
            return View(await Mediator.Send(new GetEmployeeListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailVm>> Detailed(int id)
        {
            return await Mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDetailVm>> Edit(int id)
        {
            return await Mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new { });
        }

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateEmployeeCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}