using FrontiersDemo.Domain.Entities;
using FrontiersDemo.Domain.ValueObjects;

namespace FrontiersDemo.Domain.Tests.Entities;

public sealed class UserTests
{
    private static readonly Organization ValidOrganization =
        new(1L, "Warsaw University of Technology", null, null, null, null, 201.38);

    [Fact]
    public void Constructor_NullUserName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new User(null!, 5, ValidOrganization));
    }

    [Fact]
    public void Constructor_EmptyUserName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new User(string.Empty, 5, ValidOrganization));
    }

    [Fact]
    public void Constructor_WhitespaceUserName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            new User("   ", 5, ValidOrganization));
    }

    [Fact]
    public void Constructor_NegativeNumberOfPublications_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new User("Jakub", -1, ValidOrganization));
    }

    [Fact]
    public void Constructor_NullOrganization_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
            new User("Jakub", 5, null!));
    }

    [Fact]
    public void Constructor_ValidParameters_SetsPropertiesCorrectly()
    {
        var user = new User("Jakub", 5, ValidOrganization);

        Assert.Equal("Jakub", user.UserName);
        Assert.Equal(5, user.NumberOfPublications);
        Assert.Equal(ValidOrganization, user.Organization);
    }
}
