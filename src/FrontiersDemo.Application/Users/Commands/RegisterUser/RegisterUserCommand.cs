using MediatR;

namespace FrontiersDemo.Application.Users.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string UserName,
    string UniversityName,
    int NumberOfPublications) : IRequest<RegisterUserResult>;
