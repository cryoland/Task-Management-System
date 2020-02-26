using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Application.Common.Exceptions;
using TMS.Domain.Entities;

namespace TMS.Application.Employees.Queries.GetEmployeeDetail
{
    public class GetEmptyEmployeeQuery : IRequest<EmployeeDetailVm>
    {
        public class GetEmptyEmployee : IRequestHandler<GetEmptyEmployeeQuery, EmployeeDetailVm>
        {

            public async Task<EmployeeDetailVm> Handle(GetEmptyEmployeeQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => new EmployeeDetailVm());
            }
        }
    }
}