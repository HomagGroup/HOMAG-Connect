using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts;

/// <summary>
/// Address data
/// </summary>
public class Address
{
    #region JsonExtensionData Member

    [JsonExtensionData]
    public Dictionary<string, object>? JsonExtensionData { get; set; }

    #endregion

    /// <summary>
    /// The type of the address
    /// </summary>
    public AddressType Type { get; set; }
    /// <summary>
    /// Gets or sets the country
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets the house number
    /// </summary>
    public string? HouseNumber { get; set; }

    /// <summary>
    /// Gets or sets the postal code
    /// </summary>
    public string? PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the street
    /// </summary>
    public string? Street { get; set; }

    /// <summary>
    /// Gets or sets the town
    /// </summary>
    public string? City { get; set; }
}