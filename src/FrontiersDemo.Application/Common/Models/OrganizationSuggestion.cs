namespace FrontiersDemo.Application.Common.Models;

public sealed record OrganizationSuggestion(
    long Id,
    string OrganizationName,
    string? Country,
    string? CountryIsoCode,
    string? City,
    string? Street,
    string? ZipCode,
    string? State,
    string? WebDomain);
