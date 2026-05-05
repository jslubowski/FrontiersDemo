using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Infrastructure.Persistence.Repositories;

public sealed class UserRepository(ApplicationDbContext db) : IUserRepository
{
    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct)
        => await db.Users.AsNoTracking().ToListAsync(ct);

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct)
        => await db.Users.FirstOrDefaultAsync(u => u.Id == id, ct);

    public Task AddAsync(User user, CancellationToken ct)
    {
        db.Users.Add(user);
        return Task.CompletedTask;
    }
}
