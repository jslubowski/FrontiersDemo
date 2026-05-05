using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Users.Queries.GetUsers;
using FrontiersDemo.Domain.Entities;
using FrontiersDemo.Domain.ValueObjects;
using NSubstitute;

namespace FrontiersDemo.Application.Tests.Users.Queries;

public sealed class GetUsersQueryHandlerTests
{
    private readonly IUserRepository _users = Substitute.For<IUserRepository>();
    private readonly GetUsersQueryHandler _handler;

    public GetUsersQueryHandlerTests()
    {
        _handler = new GetUsersQueryHandler(_users);
    }

    [Fact]
    public async Task Handle_ReturnsAllUsersMappedToDtos()
    {
        var organization = new Organization(1L, "Warsaw University of Technology", "Poland", null, "Warsaw", null, 201.38);
        IReadOnlyList<User> users = [new User("Jakub", 5, organization), new User("Anna", 2, organization)];
        _users.GetAllAsync(Arg.Any<CancellationToken>()).Returns(users);

        var result = await _handler.Handle(new GetUsersQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Equal("Jakub", result[0].UserName);
        Assert.Equal("Anna", result[1].UserName);
    }
}
