using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts;

/// <summary>
/// Address data
/// </summary>
public class Address : IExtensibleDataObject
{
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
    public string? Town { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}