using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Roles.Commands.CreateRole
{
    public class CreateRoleCommand : IRequest<long>
    {
        public UserRole RoleValue { get; set; }

        public class CreateIssueCommandHandler : IRequestHandler<CreateRoleCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateIssueCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
            {
                var entity = new Role
                {
                    RoleValue = request.RoleValue,
                };

                _context.Roles.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.RoleId;
            }
        }
    }
}