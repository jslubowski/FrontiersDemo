using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Infrastructure.Persistence.Repositories;

public sealed class ReviewerInvitationRepository(ApplicationDbContext db) : IReviewerInvitationRepository
{
    public async Task<IReadOnlyList<ReviewerInvitation>> GetAllAsync(CancellationToken ct)
        => await db.ReviewerInvitations.AsNoTracking().ToListAsync(ct);

    public async Task<ReviewerInvitation?> GetByIdAsync(int id, CancellationToken ct)
        => await db.ReviewerInvitations.FirstOrDefaultAsync(i => i.Id == id, ct);

    public Task AddAsync(ReviewerInvitation invitation, CancellationToken ct)
    {
        db.ReviewerInvitations.Add(invitation);
        return Task.CompletedTask;
    }
}
