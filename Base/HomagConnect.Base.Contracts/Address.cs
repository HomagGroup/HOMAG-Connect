using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Address data
/// </summary>
public class Address
{
    /// <summary>
    /// The type of the address.
    /// </summary>
    [JsonProperty(Order = 0)]
    public AddressType Type { get; set; } = AddressType.Unknown;

    /// <summary>
    /// Gets or sets the country
    /// </summary>
    [JsonProperty(Order = 5)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the house number.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the street.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}