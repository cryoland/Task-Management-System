using FluentValidation;

namespace TMS.Application.Identity.Commands.CreateAppUser
{
    public class CreateAppUserCommandValidator : AbstractValidator<CreateAppUserCommand>
    {
        public CreateAppUserCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty();
            RuleFor(e => e.Password)
                .NotEmpty();
        }
    }
}
