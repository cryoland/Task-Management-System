using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;

namespace TMS.Application.Identity.Commands.DeleteAppUser
{
    public class DeleteAppUserCommand : IRequest
    {
        public string AppUserId { get; set; }

        public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand>
        {
            private readonly IIdentityService _identityService;

            public DeleteAppUserCommandHandler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
            {
                if (!await _identityService.IsUserExistAsync(request.AppUserId))
                {
                    await _identityService.DeleteUserAsync(request.AppUserId);
                }
                return Unit.Value;
            }
        }
    }
}
