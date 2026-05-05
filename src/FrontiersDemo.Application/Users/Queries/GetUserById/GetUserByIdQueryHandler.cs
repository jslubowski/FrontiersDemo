using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Users.Dtos;
using FrontiersDemo.Domain.Entities;
using MediatR;

namespace FrontiersDemo.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler(IUserRepository users)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(request.Id, ct);
        if (user is null)
            throw new NotFoundException(nameof(User), request.Id);

        return new UserDto(
            user.Id,
            user.UserName,
            user.NumberOfPublications,
            user.Organization.FrontiersId,
            user.Organization.Name,
            user.Organization.Country,
            user.Organization.City);
    }
}
