using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;
using TMS.Domain.Enumerations;

namespace TMS.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<long>
    {
        public string AppUserId { get; set; }

        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, long>
        {
            private readonly IApplicationDbContext _context;

            public CreateEmployeeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<long> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
            {
                var entity = new Employee
                {
                    ShortName = request.ShortName,
                    FullName = request.FullName,
                    AppUserId = request.AppUserId,
                };

                _context.Employees.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.EmployeeId;
            }
        }
    }
}
