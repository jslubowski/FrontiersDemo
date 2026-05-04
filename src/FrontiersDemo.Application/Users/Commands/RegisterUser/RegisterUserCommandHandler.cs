using FluentValidation.Results;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Users.Dtos;
using FrontiersDemo.Domain.Entities;
using FrontiersDemo.Domain.ValueObjects;
using MediatR;
using ValidationException = FrontiersDemo.Application.Common.Exceptions.ValidationException;

namespace FrontiersDemo.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler(
    IApplicationDbContext db,
    IFrontiersOrganizationsClient frontiers)
    : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        var match = await frontiers.SearchAsync(request.UniversityName, ct);
        if (match is null)
        {
            var failure = new ValidationFailure(
                nameof(request.UniversityName),
                $"University '{request.UniversityName}' was not found in the Frontiers organizations directory.");
            throw new ValidationException([failure]);
        }

        var organization = new Organization(
            match.Id, match.OrganizationName, match.Country, match.CountryIsoCode, match.City, match.WebDomain);

        var user = new User(request.UserName, request.NumberOfPublications, organization);
        db.Users.Add(user);
        await db.SaveChangesAsync(ct);

        return new RegisterUserResult(new UserDto(
            user.Id, user.UserName, user.NumberOfPublications,
            organization.FrontiersId, organization.Name, organization.Country, organization.City));
    }
}
