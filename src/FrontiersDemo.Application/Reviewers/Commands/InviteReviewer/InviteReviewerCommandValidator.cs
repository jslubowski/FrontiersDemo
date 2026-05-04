using FluentValidation;

namespace FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;

public sealed class InviteReviewerCommandValidator : AbstractValidator<InviteReviewerCommand>
{
    public InviteReviewerCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
    }
}
