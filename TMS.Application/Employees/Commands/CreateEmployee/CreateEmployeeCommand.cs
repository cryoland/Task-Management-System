using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<long>
    {
        public string ShortName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

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
                    Email = request.Email,
                    Password = request.Password,
                    Active = true,
                    RoleId = request.RoleId,
                };

                _context.Employees.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return entity.EmployeeId;
            }
        }
    }
}