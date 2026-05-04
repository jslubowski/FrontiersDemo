using FrontiersDemo.Application.Common.Models;

namespace FrontiersDemo.Application.Common.Interfaces;

public interface IFrontiersOrganizationsClient
{
    Task<OrganizationSuggestion?> SearchAsync(string query, CancellationToken ct);
}
