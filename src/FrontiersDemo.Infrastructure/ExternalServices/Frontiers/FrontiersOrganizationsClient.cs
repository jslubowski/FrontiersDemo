using System.Net.Http.Json;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Common.Models;
using Microsoft.Extensions.Options;

namespace FrontiersDemo.Infrastructure.ExternalServices.Frontiers;

public sealed class FrontiersOrganizationsClient(HttpClient http, IOptions<FrontiersApiOptions> options)
    : IFrontiersOrganizationsClient
{
    private readonly FrontiersApiOptions _options = options.Value;

    public async Task<OrganizationSuggestion?> SearchAsync(string query, CancellationToken ct)
    {
        var url = $"/v1/organizations/elasticSuggestions?query={Uri.EscapeDataString(query)}&maxcount={_options.MaxCount}";
        var results = await http.GetFromJsonAsync<List<FrontiersResponseItem>>(url, ct);
        var first = results?.FirstOrDefault();
        if (first is null) return null;

        return new OrganizationSuggestion(
            first.Id, first.OrganizationName ?? string.Empty,
            first.Country, first.CountryIsoCode, first.City,
            first.Street, first.ZipCode, first.State, first.WebDomain);
    }

    private sealed record FrontiersResponseItem(
        long Id,
        string? OrganizationName,
        string? Country,
        string? CountryIsoCode,
        string? City,
        string? Street,
        string? ZipCode,
        string? State,
        string? WebDomain);
}
