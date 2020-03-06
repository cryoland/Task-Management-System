using FluentValidation;

namespace TMS.Application.Identity.Commands.AssignAppUserRole
{
    public class AssignAppUserRoleCommandValidator : AbstractValidator<AssignAppUserRoleCommand>
    {
        public AssignAppUserRoleCommandValidator()
        {
            RuleFor(e => e.AppUserId)
                .NotEmpty();
            RuleFor(e => e.Role)
                .NotEmpty();
        }
    }
}
