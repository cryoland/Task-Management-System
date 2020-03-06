using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Exceptions;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest
    {
        public long Id { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }

        public UserRole Role { get; set; }

        class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
        {
            private readonly IApplicationDbContext _context;

            public UpdateEmployeeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Employees.FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Employee), request.Id);
                }

                entity.ShortName = request.ShortName;
                entity.FullName = request.FullName;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
