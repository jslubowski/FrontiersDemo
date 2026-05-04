using FrontiersDemo.Domain.ValueObjects;

namespace FrontiersDemo.Domain.Entities;

public sealed class User
{
    public int Id { get; private set; }
    public string UserName { get; private set; } = string.Empty;
    public int NumberOfPublications { get; private set; }
    public Organization Organization { get; private set; } = null!;

    private User() { }

    public User(string userName, int numberOfPublications, Organization organization)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName must not be empty.", nameof(userName));
        if (numberOfPublications < 0)
            throw new ArgumentOutOfRangeException(nameof(numberOfPublications), "Must be >= 0.");

        UserName = userName;
        NumberOfPublications = numberOfPublications;
        Organization = organization ?? throw new ArgumentNullException(nameof(organization));
    }
}
