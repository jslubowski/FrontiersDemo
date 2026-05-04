using FrontiersDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<ReviewerInvitation> ReviewerInvitations { get; }

    Task<int> SaveChangesAsync(CancellationToken ct);
}
