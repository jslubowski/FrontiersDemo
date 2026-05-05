using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Domain.Entities;
using MediatR;

namespace FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;

public sealed class InviteReviewerCommandHandler(
    IUserRepository users,
    IReviewerInvitationRepository invitations,
    IUnitOfWork unitOfWork,
    TimeProvider clock)
    : IRequestHandler<InviteReviewerCommand, InviteReviewerResult>
{
    private const int MinPublications = 3;
    private const double MinUniversityScore = 60;

    public async Task<InviteReviewerResult> Handle(InviteReviewerCommand request, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(request.UserId, ct);
        if (user is null)
            throw new NotFoundException(nameof(User), request.UserId);

        if (user.NumberOfPublications <= MinPublications || user.Organization.Score < MinUniversityScore)
        {
            return new InviteReviewerResult(
                false,
                $"The invitation could not be sent. The reviewer must have more than {MinPublications} publications and a university score of at least {MinUniversityScore}.");
        }

        var invitation = new ReviewerInvitation(request.UserId, clock.GetUtcNow());
        await invitations.AddAsync(invitation, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return new InviteReviewerResult(true, "The invitation was sent successfully.", invitation.Id);
    }
}
