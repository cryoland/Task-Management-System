using FluentValidation;

namespace TMS.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(64);
            RuleFor(e => e.Password)
                .NotEmpty();
            RuleFor(e => e.FullName)
                .NotEmpty();
        }
    }
}