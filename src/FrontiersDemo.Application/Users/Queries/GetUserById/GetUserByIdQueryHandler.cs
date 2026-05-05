using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Users.Dtos;
using FrontiersDemo.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FrontiersDemo.Application.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler(IApplicationDbContext db)
    : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var user = await db.Users
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .Select(u => new UserDto(
                u.Id,
                u.UserName,
                u.NumberOfPublications,
                u.Organization.FrontiersId,
                u.Organization.Name,
                u.Organization.Country,
                u.Organization.City))
            .FirstOrDefaultAsync(ct);

        return user ?? throw new NotFoundException(nameof(User), request.Id);
    }
}
