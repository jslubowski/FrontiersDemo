using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Reviewers.Dtos;
using FrontiersDemo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Application.Reviewers.Queries.GetReviewerInvitationById;

public sealed class GetReviewerInvitationByIdQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetReviewerInvitationByIdQuery, ReviewerInvitationDto>
{
    public async Task<ReviewerInvitationDto> Handle(GetReviewerInvitationByIdQuery request, CancellationToken ct)
    {
        var dto = await db.ReviewerInvitations
            .AsNoTracking()
            .Where(i => i.Id == request.Id)
            .Select(i => new ReviewerInvitationDto(i.Id, i.UserId, i.InvitedAt))
            .FirstOrDefaultAsync(ct);

        return dto ?? throw new NotFoundException(nameof(ReviewerInvitation), request.Id);
    }
}
