namespace FrontiersDemo.Domain.ValueObjects;

public sealed class Organization
{
    public long FrontiersId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Country { get; private set; }
    public string? CountryIsoCode { get; private set; }
    public string? City { get; private set; }
    public string? WebDomain { get; private set; }

    private Organization() { }

    public Organization(long frontiersId, string name, string? country, string? countryIsoCode, string? city, string? webDomain)
    {
        FrontiersId = frontiersId;
        Name = name;
        Country = country;
        CountryIsoCode = countryIsoCode;
        City = city;
        WebDomain = webDomain;
    }
}
