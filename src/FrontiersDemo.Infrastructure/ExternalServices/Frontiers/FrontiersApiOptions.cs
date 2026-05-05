namespace FrontiersDemo.Infrastructure.ExternalServices.Frontiers;

public sealed class FrontiersApiOptions
{
    public const string SectionName = "Frontiers";

    public string BaseUrl { get; set; } = "https://organizations-api.frontiersin.org";
}
