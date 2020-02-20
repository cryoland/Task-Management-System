using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommand : IRequest<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public PriorityLevel Priority { get; set; }

        public int AssigneeId { get; set; }

        public int ReporterId { get; set; }

        public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateIssueCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
            {
                var entity = new Issue
                {
                    Name = request.Name,
                    Description = request.Description,
                    Priority = request.Priority,
                    AssigneeId = request.AssigneeId,
                    ReporterId = request.ReporterId,
                };

                _context.Issues.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.IssueId;
            }
        }
    }
}