using FrontiersDemo.Application.Common.Exceptions;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Users.Queries.GetUserById;
using FrontiersDemo.Domain.Entities;
using FrontiersDemo.Domain.ValueObjects;
using NSubstitute;

namespace FrontiersDemo.Application.Tests.Users.Queries;

public sealed class GetUserByIdQueryHandlerTests
{
    private readonly IUserRepository _users = Substitute.For<IUserRepository>();
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _handler = new GetUserByIdQueryHandler(_users);
    }

    [Fact]
    public async Task Handle_UserNotFound_ThrowsNotFoundException()
    {
        _users.GetByIdAsync(99, Arg.Any<CancellationToken>()).Returns((User?)null);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(new GetUserByIdQuery(99), CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UserFound_ReturnsDto()
    {
        var organization = new Organization(1L, "Warsaw University of Technology", "Poland", null, "Warsaw", null, 201.38);
        var user = new User("Jakub", 5, organization);
        _users.GetByIdAsync(1, Arg.Any<CancellationToken>()).Returns(user);

        var result = await _handler.Handle(new GetUserByIdQuery(1), CancellationToken.None);

        Assert.Equal("Jakub", result.UserName);
        Assert.Equal(5, result.NumberOfPublications);
        Assert.Equal(1L, result.OrganizationId);
        Assert.Equal("Warsaw University of Technology", result.OrganizationName);
    }
}
