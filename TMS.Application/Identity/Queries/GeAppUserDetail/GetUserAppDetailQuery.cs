using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMS.Application.Common.Exceptions;
using TMS.Application.Common.Interfaces;
using TMS.Domain.Entities;

namespace TMS.Application.Identity.Queries.GeAppUserDetail
{
    public class GetUserAppDetailQuery : IRequest<string>
    {
        public string AppUserId { get; set; }

        public class GetUserAppDetailQueryHandler : IRequestHandler<GetUserAppDetailQuery, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IIdentityService _identityService;

            public GetUserAppDetailQueryHandler(IApplicationDbContext context, IMapper mapper, IIdentityService identityService)
            {
                _context = context;
                _mapper = mapper;
                _identityService = identityService;
            }

            public async Task<string> Handle(GetUserAppDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _identityService.GetUserEmailAsync(request.AppUserId);

                if (vm == null)
                {
                    throw new NotFoundException(nameof(Employee), request.AppUserId);
                }

                return vm;
            }
        }
    }
}
