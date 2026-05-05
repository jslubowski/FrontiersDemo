using System.Net.Http.Json;
using FrontiersDemo.Application.Common.Interfaces;
using FrontiersDemo.Application.Common.Models;
using Microsoft.Extensions.Options;

namespace FrontiersDemo.Infrastructure.ExternalServices.Frontiers;

public sealed class FrontiersOrganizationsClient(HttpClient http, IOptions<FrontiersApiOptions> options)
    : IFrontiersOrganizationsClient
{
    private const int MaxCount = 1;
    private readonly FrontiersApiOptions _options = options.Value;

    public async Task<OrganizationSuggestion?> SearchAsync(string query, CancellationToken ct)
    {
        var url = $"/v1/organizations/elasticSuggestions?query={Uri.EscapeDataString(query)}&maxcount={MaxCount}";
        var results = await http.GetFromJsonAsync<List<FrontiersResponseItem>>(url, ct);
        var first = results?.FirstOrDefault();
        if (first is null) return null;

        return new OrganizationSuggestion(
            first.Id, first.OrganizationName ?? string.Empty,
            first.Country, first.CountryIsoCode, first.City,
            first.Street, first.ZipCode, first.State, first.WebDomain,
            first.Score);
    }
}
