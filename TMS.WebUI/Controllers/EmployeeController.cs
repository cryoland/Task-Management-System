using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Application.Employees.Commands.CreateEmployee;
using TMS.Application.Employees.Commands.DeleteEmployee;
using TMS.Application.Employees.Commands.UpdateEmployee;
using TMS.Application.Employees.Queries.GetEmployeeDetail;
using TMS.Application.Employees.Queries.GetEmployeeList;

namespace TMS.WebUI.Controllers
{
    public class EmployeeController : ContentController
    {
        public async Task<ActionResult> Index()
        {            
            return View(await Mediator.Send(new GetEmployeeListQuery()));
        }

        public async Task<ActionResult> Detailed([FromRoute]int id)
        {
            return View(await Mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id }));
        }

        public async Task<ActionResult> Edit([FromRoute]int id)
        {
            return View(await Mediator.Send(new GetEmployeeDetailQuery { EmployeeId = id }));
        }

        public async Task<ActionResult> Create()
        {
            return View(await Mediator.Send(new GetEmptyEmployeeQuery()));
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateEmployeeCommand command)
        {
            var result = await Mediator.Send(command);
            if(result > 0)
            {
                return RedirectToAction(nameof(this.Index));
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult> Update(long id, UpdateEmployeeCommand command)
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
            await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            return RedirectToAction(nameof(this.Index));
        }
    }
}