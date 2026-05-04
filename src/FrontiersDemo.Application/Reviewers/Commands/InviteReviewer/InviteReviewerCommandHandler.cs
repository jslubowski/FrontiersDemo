using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Application.Reviewers.Commands.InviteReviewer;

public sealed class InviteReviewerCommandHandler(IApplicationDbContext db, TimeProvider clock)
    : IRequestHandler<InviteReviewerCommand, int>
{
    public async Task<int> Handle(InviteReviewerCommand request, CancellationToken ct)
    {
        var userExists = await db.Users.AnyAsync(u => u.Id == request.UserId, ct);
        if (!userExists)
            throw new NotFoundException(nameof(User), request.UserId);

        var invitation = new ReviewerInvitation(request.UserId, clock.GetUtcNow());
        db.ReviewerInvitations.Add(invitation);
        await db.SaveChangesAsync(ct);
        return invitation.Id;
    }
}
