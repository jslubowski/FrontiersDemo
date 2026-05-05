using FrontiersDemo.Application.Users.Dtos;
using MediatR;

namespace FrontiersDemo.Application.Users.Queries.GetUsers;

public sealed record GetUsersQuery : IRequest<IReadOnlyList<UserDto>>;
