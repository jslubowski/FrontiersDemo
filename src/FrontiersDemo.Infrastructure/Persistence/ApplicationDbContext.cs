using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext, IUnitOfWork
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ReviewerInvitation> ReviewerInvitations => Set<ReviewerInvitation>();

    async Task IUnitOfWork.SaveChangesAsync(CancellationToken ct) => await SaveChangesAsync(ct);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
