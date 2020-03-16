using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using TMS.Application.Common.Exceptions;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.Issues.Queries.GetIssueDetail
{
    public class GetIssueDetailQuery : IRequest<IssueDetailVm>
    {
        public long IssueId { get; set; }

        public class GetIssueDetailQueryHandler : IRequestHandler<GetIssueDetailQuery, IssueDetailVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIssueDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueDetailVm> Handle(GetIssueDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Issues
                    .ProjectTo<IssueDetailVm>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(p => p.Id == request.IssueId, cancellationToken);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Issue), request.IssueId);
                }

                return vm;
            }
        }
    }
}
