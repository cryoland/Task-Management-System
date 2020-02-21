using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TMS.Application.Common.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TMS.Application.Employees.Queries.GetEmployeeList
{
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, EmployeeListVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetEmployeeListQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<EmployeeListVm> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees
                .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.FullName)
                .ToListAsync(cancellationToken);

            var vm = new EmployeeListVm
            {
                Employees = employees,
            };

            return vm;
        }
    }
}