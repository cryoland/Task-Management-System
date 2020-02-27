using AutoMapper.QueryableExtensions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TMS.Application.Common.Interfaces;

namespace TMS.Application.Issues.Queries.GetIssueList
{
    public class GetIssueListQuery : IRequest<IssueListVm>
    {
        class GetIssueListQueryHandler : IRequestHandler<GetIssueListQuery, IssueListVm>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetIssueListQueryHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<IssueListVm> Handle(GetIssueListQuery request, CancellationToken cancellationToken)
            {
                var issues = await _context.Issues
                    .ProjectTo<IssueDto>(_mapper.ConfigurationProvider)
                    .OrderBy(p => p.Name)
                    .ToListAsync(cancellationToken);

                var vm = new IssueListVm
                {
                    Issues = issues
                };

                return vm;
            }
        }
    }
}