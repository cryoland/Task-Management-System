using FluentValidation;

namespace TMS.Application.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
    {
        public UpdateIssueCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(64);
            RuleFor(e => e.Description)
                .MaximumLength(256);
            RuleFor(e => e.AssigneeId)
                .NotEmpty();
            RuleFor(e => e.ReporterId)
                .NotEmpty();
            RuleFor(e => e.Priority)
                .NotEmpty();
        }
    }
}