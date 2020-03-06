using FluentValidation;

namespace TMS.Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueCommandValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(i => i.Description)
                .MaximumLength(256);
            RuleFor(i => i.AssigneeId)
                .NotEmpty();
            RuleFor(i => i.ReporterId)
                .NotEmpty();
            RuleFor(i => i.Priority)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
