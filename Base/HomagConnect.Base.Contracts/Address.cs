using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Represents a postal address used by API contracts.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the address type.
    /// Supported values: <c>Unknown</c>, <c>Delivery</c>, <c>Billing</c>, <c>DeliveryAndBilling</c>.
    /// </summary>
    /// <example>Delivery</example>
    [JsonProperty(Order = 0)]
    public AddressType Type { get; set; } = AddressType.Unknown;

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    /// <example>Germany</example>
    [JsonProperty(Order = 6)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the house number.
    /// </summary>
    /// <example>12A</example>
    [JsonProperty(Order = 3)]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Gets or sets the postal code.
    /// </summary>
    /// <example>72622</example>
    [JsonProperty(Order = 4)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <example>HOMAG GmbH</example>
    [JsonProperty(Order = 1)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the street.
    /// </summary>
    /// <example>Homagstrasse</example>
    [JsonProperty(Order = 2)]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    /// <example>Nuertingen</example>
    [JsonProperty(Order = 5)]
    public string? City { get; set; }

    /// <summary>
    /// Gets or sets additional custom properties.
    /// Any JSON properties not mapped to typed members can be captured here via <c>[JsonExtensionData]</c>.
    /// </summary>
    /// <example>{ "contactPerson": "Max Mustermann" }</example>
    [JsonProperty(Order = 80)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the additional info.
    /// </summary>
    /// <example>2nd floor</example>
    public string? AdditionalInfo { get; set; }

    /// <summary>
    /// Gets or sets the delivery distance in miles.
    /// </summary>
    [JsonProperty(Order = 10)]
    public decimal? DeliveryDistance { get; set; }
}