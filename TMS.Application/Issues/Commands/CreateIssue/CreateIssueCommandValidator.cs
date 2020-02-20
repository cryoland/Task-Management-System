using FluentValidation;

namespace TMS.Application.Issues.Commands.CreateIssue
{
    class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueCommandValidator()
        {
            RuleFor(i => i.Name)
                .MaximumLength(64)
                .NotEmpty();
            RuleFor(i => i.AssigneeId)
                .NotEmpty();
            RuleFor(i => i.ReporterId)
                .NotEmpty();
            RuleFor(i => i.Priority)
                .NotEmpty();
        }
    }
}