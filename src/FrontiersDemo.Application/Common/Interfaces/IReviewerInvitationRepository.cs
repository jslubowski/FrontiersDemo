using FrontiersDemo.Domain.Entities;

namespace FrontiersDemo.Application.Common.Interfaces;

public interface IReviewerInvitationRepository
{
    Task<IReadOnlyList<ReviewerInvitation>> GetAllAsync(CancellationToken ct);
    Task<ReviewerInvitation?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(ReviewerInvitation invitation, CancellationToken ct);
}
