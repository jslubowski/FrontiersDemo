using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<ReviewerInvitation> ReviewerInvitations => Set<ReviewerInvitation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
