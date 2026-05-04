namespace FrontiersDemo.Application.Users.Dtos;

public sealed record UserDto(
    int Id,
    string UserName,
    int NumberOfPublications,
    long OrganizationId,
    string OrganizationName,
    string? Country,
    string? City);
