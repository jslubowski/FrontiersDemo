using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Users.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetUsersQuery, IReadOnlyList<UserDto>>
{
    public async Task<IReadOnlyList<UserDto>> Handle(GetUsersQuery request, CancellationToken ct)
    {
        return await db.Users
            .AsNoTracking()
            .Select(u => new UserDto(
                u.Id,
                u.UserName,
                u.NumberOfPublications,
                u.Organization.FrontiersId,
                u.Organization.Name,
                u.Organization.Country,
                u.Organization.City))
            .ToListAsync(ct);
    }
}
