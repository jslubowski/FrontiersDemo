using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;

public sealed class InviteReviewerCommandHandler(IApplicationDbContext db, TimeProvider clock)
    : IRequestHandler<InviteReviewerCommand, InviteReviewerResult>
{
    private const int MinPublications = 3;
    private const double MinUniversityScore = 60;

    public async Task<InviteReviewerResult> Handle(InviteReviewerCommand request, CancellationToken ct)
    {
        var user = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, ct);
        if (user is null)
            throw new NotFoundException(nameof(User), request.UserId);

        if (user.NumberOfPublications <= MinPublications || user.Organization.Score < MinUniversityScore)
        {
            return new InviteReviewerResult(
                false,
                $"The invitation could not be sent. The reviewer must have more than {MinPublications} publications and a university score of at least {MinUniversityScore}.");
        }

        var invitation = new ReviewerInvitation(request.UserId, clock.GetUtcNow());
        db.ReviewerInvitations.Add(invitation);
        await db.SaveChangesAsync(ct);

        return new InviteReviewerResult(true, "The invitation was sent successfully.", invitation.Id);
    }
}
