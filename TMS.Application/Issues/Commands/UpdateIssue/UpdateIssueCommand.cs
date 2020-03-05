using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Exceptions;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommand : IRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public PriorityLevel Priority { get; set; }

        public IssueStatus Status { get; set; }

        public long AssigneeId { get; set; }

        public long ReporterId { get; set; }

        class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateIssueCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Issues.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Issue), request.Id);
                }

                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.Priority = request.Priority;
                entity.AssigneeId = request.AssigneeId;
                entity.ReporterId = request.ReporterId;
                entity.Status = request.Status;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }               
    }
}