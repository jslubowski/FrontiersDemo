using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Reviewers.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitations;

public sealed class GetReviewerInvitationsQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetReviewerInvitationsQuery, IReadOnlyList<ReviewerInvitationDto>>
{
    public async Task<IReadOnlyList<ReviewerInvitationDto>> Handle(GetReviewerInvitationsQuery request, CancellationToken ct)
    {
        return await db.ReviewerInvitations
            .AsNoTracking()
            .Select(i => new ReviewerInvitationDto(i.Id, i.UserId, i.InvitedAt))
            .ToListAsync(ct);
    }
}
