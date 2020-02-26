using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    }
}