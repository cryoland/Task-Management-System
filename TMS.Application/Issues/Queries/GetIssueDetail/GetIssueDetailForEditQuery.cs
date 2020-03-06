using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TMS.Application.Common.Exceptions;
using TMS.Application.Common.Interfaces;
using TMS.Application.Common.Models;
using TMS.Domain.Entities;

namespace TMS.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailForEditQuery : IRequest<IssueDetailForUpsertVm>
    {
        public long IssueId { get; set; }

        class GetIssueDetailForEditQueryHandler : IRequestHandler<GetIssueDetailForEditQuery, IssueDetailForUpsertVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIssueDetailForEditQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueDetailForUpsertVm> Handle(GetIssueDetailForEditQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Issues
                    .ProjectTo<IssueDetailForUpsertVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.Id == request.IssueId, cancellationToken);

                var employees = await _context.Employees                    
                    .Select(e => new FrameDto { Value = e.EmployeeId, Name = e.FullName })
                    .ToListAsync(cancellationToken);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Issue), request.IssueId);
                }

                vm.Employees = employees;

                return vm;
            }
        }
    }
}
