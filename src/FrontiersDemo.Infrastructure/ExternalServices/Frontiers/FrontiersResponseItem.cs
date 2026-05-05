namespace FrontiersDemo.Infrastructure.ExternalServices.Frontiers;

internal sealed record FrontiersResponseItem(
    long Id,
    string? OrganizationName,
    string? Country,
    string? CountryIsoCode,
    string? City,
    string? Street,
    string? ZipCode,
    string? State,
    string? WebDomain,
    double Score);