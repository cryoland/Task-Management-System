using FluentValidation;

namespace TMS.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(64);                
            RuleFor(e => e.Password)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(e => e.FullName)
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}