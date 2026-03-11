namespace HomagConnect.Base.Contracts;

/// <summary>
/// Represents a storage location or compartment used by a machine or storage system.
/// </summary>
public class StorageLocation
{
    /// <summary>
    /// Gets or sets the barcode of the compartment.
    /// </summary>
    /// <example>COMP-0004711</example>
    public string Barcode { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the storage location.
    /// </summary>
    /// <example>LOC-01-02-03</example>
    public string LocationId { get; set; }

    /// <summary>
    /// Gets or sets the display name of the storage location.
    /// </summary>
    /// <example>Main Buffer 03</example>
    public string Name { get; set; }
}