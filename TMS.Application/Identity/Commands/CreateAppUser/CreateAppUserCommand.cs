using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Interfaces;

namespace TMS.Application.Identity.Commands.CreateAppUser
{
    public class CreateAppUserCommand : IRequest<string>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, string>
        {
            private readonly IIdentityService _identityService;

            public CreateAppUserCommandHandler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<string> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
            {
                if (!await _identityService.IsUserExistAsync(request.Email))
                {
                    var (Result, UserId) = await _identityService.CreateUserAsync(request.Email, request.Password);
                    
                    if (Result.Succeeded)
                    {
                        return UserId;
                    }
                }
                return string.Empty;
            }
        }
    }
}
