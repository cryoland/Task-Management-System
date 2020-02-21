using MediatR;

namespace TMS.Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailVm>
    {
        public int EmployeeId { get; set; }
    }
}