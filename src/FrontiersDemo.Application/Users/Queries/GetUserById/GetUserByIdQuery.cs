using FrontiersDemo.Application.Users.Dtos;
using MediatR;

namespace FrontiersDemo.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(int Id) : IRequest<UserDto>;
