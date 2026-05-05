using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Reviewers.Dtos;
using FrontiersDemo.Domain.Entities;
using MediatR;

namespace FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitationById;

public sealed class GetReviewerInvitationByIdQueryHandler(IReviewerInvitationRepository invitations)
    : IRequestHandler<GetReviewerInvitationByIdQuery, ReviewerInvitationDto>
{
    public async Task<ReviewerInvitationDto> Handle(GetReviewerInvitationByIdQuery request, CancellationToken ct)
    {
        var invitation = await invitations.GetByIdAsync(request.Id, ct);
        if (invitation is null)
            throw new NotFoundException(nameof(ReviewerInvitation), request.Id);

        return new ReviewerInvitationDto(invitation.Id, invitation.UserId, invitation.InvitedAt);
    }
}
