using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Reviewers.Dtos;
using MediatR;

namespace FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitations;

public sealed class GetReviewerInvitationsQueryHandler(IReviewerInvitationRepository invitations)
    : IRequestHandler<GetReviewerInvitationsQuery, IReadOnlyList<ReviewerInvitationDto>>
{
    public async Task<IReadOnlyList<ReviewerInvitationDto>> Handle(GetReviewerInvitationsQuery request, CancellationToken ct)
    {
        var result = await invitations.GetAllAsync(ct);
        return result.Select(i => new ReviewerInvitationDto(i.Id, i.UserId, i.InvitedAt)).ToList();
    }
}
