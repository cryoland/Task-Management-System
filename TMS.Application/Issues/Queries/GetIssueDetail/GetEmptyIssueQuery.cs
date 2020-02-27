using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Application.Common.Models;

namespace TMS.Application.Issues.Queries.GetIssueDetail
{
    public class GetEmptyIssueQuery : IRequest<IssueDetailForUpsertVm>
    {
        class GetEmptyIssueQueryHandler : IRequestHandler<GetEmptyIssueQuery, IssueDetailForUpsertVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEmptyIssueQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueDetailForUpsertVm> Handle(GetEmptyIssueQuery request, CancellationToken cancellationToken)
            {
                var employees = await _context.Employees
                    .Select(e => new FrameDto { Value = e.EmployeeId, Name = e.FullName })
                    .ToListAsync(cancellationToken);

                var vm = new IssueDetailForUpsertVm 
                { 
                    Employees = employees, 
                    Name = string.Empty, 
                    AssigneeName = string.Empty, 
                    ReporterName = string.Empty,
                    Description = string.Empty,                     
                };

                return vm;
            }
        }
    }
}