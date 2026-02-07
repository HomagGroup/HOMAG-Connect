using Newtonsoft.Json;
using System.Collections.ObjectModel;

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
    public List<AddressType> Type { get; set; } = new List<AddressType>();

    /// <summary>
    /// Gets or sets the country
    /// </summary>
    [JsonProperty(Order = 6)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the house number.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the street.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    [JsonProperty(Order = 5)]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}