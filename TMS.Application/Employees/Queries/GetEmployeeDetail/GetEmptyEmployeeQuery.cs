using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmptyEmployeeQuery : IRequest<EmployeeDetailVm>
    {
        class GetEmptyEmployeeQueryHandler : IRequestHandler<GetEmptyEmployeeQuery, EmployeeDetailVm>
        {
            public async Task<EmployeeDetailVm> Handle(GetEmptyEmployeeQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => new EmployeeDetailVm());
            }
        }
    }
}
