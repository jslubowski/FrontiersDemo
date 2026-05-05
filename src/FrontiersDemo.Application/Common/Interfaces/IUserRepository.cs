using FrontiersDemo.Domain.Entities;

namespace FrontiersDemo.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct);
    Task<User?> GetByIdAsync(int id, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
}
