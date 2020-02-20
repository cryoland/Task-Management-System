using FluentValidation;

namespace TMS.Application.Roles.Commands.CreateRole
{
    class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(r => r.RoleValue)
                .NotEmpty();
        }
    }
}