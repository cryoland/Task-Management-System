using FluentValidation;

namespace TMS.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty();
            RuleFor(e => e.Password)
                .NotEmpty();
            RuleFor(e => e.FullName)
                .NotEmpty();
        }
    }
}