using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders;

/// <summary>
/// Address data
/// </summary>
public class Address : IExtensibleDataObject
{
    /// <summary>
    /// Gets or sets the country
    /// </summary>
    [JsonProperty(Order = 5)]
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the house number
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Gets or sets the postal code
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the street
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the town
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? City { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}