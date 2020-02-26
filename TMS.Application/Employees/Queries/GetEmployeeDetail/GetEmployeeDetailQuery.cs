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
    public class GetEmployeeDetailQuery : IRequest<EmployeeDetailVm>
    {
        public int EmployeeId { get; set; }

        public class GetEmployeeDetailQueryHandler : IRequestHandler<GetEmployeeDetailQuery, EmployeeDetailVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEmployeeDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<EmployeeDetailVm> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Employees
                    .ProjectTo<EmployeeDetailVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.EmployeeId == request.EmployeeId, cancellationToken);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Employee), request.EmployeeId);
                }

                return vm;
            }
        }
    }
}