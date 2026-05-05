using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Users.Dtos;
using MediatR;

namespace FrontiersDemo.Application.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler(IUserRepository users)
    : IRequestHandler<GetUsersQuery, IReadOnlyList<UserDto>>
{
    public async Task<IReadOnlyList<UserDto>> Handle(GetUsersQuery request, CancellationToken ct)
    {
        var result = await users.GetAllAsync(ct);
        return result.Select(u => new UserDto(
            u.Id,
            u.UserName,
            u.NumberOfPublications,
            u.Organization.FrontiersId,
            u.Organization.Name,
            u.Organization.Country,
            u.Organization.City))
            .ToList();
    }
}
