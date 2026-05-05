using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Common.Models;
using FrontiersDemo.Application.Users.Commands.RegisterUser;
using FrontiersDemo.Domain.Entities;
using NSubstitute;
using ValidationException = FrontiersDemo.Application.Common.Exceptions.ValidationException;

namespace FrontiersDemo.Application.Tests.Users.Commands;

public sealed class RegisterUserCommandHandlerTests
{
    private readonly IUserRepository _users = Substitute.For<IUserRepository>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IFrontiersOrganizationsClient _frontiers = Substitute.For<IFrontiersOrganizationsClient>();
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _handler = new RegisterUserCommandHandler(_users, _unitOfWork, _frontiers);
    }

    [Fact]
    public async Task Handle_UniversityNotFound_ThrowsValidationException()
    {
        _frontiers.SearchAsync("Unknown University", Arg.Any<CancellationToken>()).Returns((OrganizationSuggestion?)null);

        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(new RegisterUserCommand("Jakub", "Unknown University", 5), CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UniversityFound_CreatesUserAndReturnsResult()
    {
        var suggestion = new OrganizationSuggestion(1L, "Warsaw University of Technology", "Poland", "POL", "Warsaw", null, null, null, null, 201.38);
        _frontiers.SearchAsync("Warsaw University of Technology", Arg.Any<CancellationToken>()).Returns(suggestion);

        var result = await _handler.Handle(
            new RegisterUserCommand("Jakub", "Warsaw University of Technology", 5), CancellationToken.None);

        Assert.Equal("Jakub", result.User.UserName);
        Assert.Equal(5, result.User.NumberOfPublications);
        Assert.Equal(1L, result.User.OrganizationId);
        await _users.Received(1).AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
        await _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
