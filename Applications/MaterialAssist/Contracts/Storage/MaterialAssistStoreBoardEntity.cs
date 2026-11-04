using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.MaterialAssist.Contracts.Storage;

/// <summary>
/// Destination and optional measured dimensions for storing an existing board entity.
/// Measured dimensions apply when storing a single board.
/// </summary>
/// <example>
/// {
/// "id": "board-001",
/// "length": 1000,
/// "width": 500,
/// "storageLocation": { "locationId": "rack-a-01" },
/// "workstation": { "id": "b3f05ef4-f34a-4f90-bd7f-7c54805c7c25" }
/// }
/// </example>
public class MaterialAssistStoreBoardEntity : IDimensionPropertiesLengthWidth
{
    /// <summary>
    /// Identifier of the existing board entity to store, not its board type code.
    /// </summary>
    /// <example>board-001</example>
    [StringLength(50)]
    public string Id { get; set; }

    /// <inheritdoc />
    public double? Length { get; set; }

    /// <summary>
    /// Destination compartment for the board, identified by its LocationId.
    /// </summary>
    public StorageLocation StorageLocation { get; set; }

    /// <inheritdoc />
    public double? Width { get; set; }

    /// <summary>
    /// Workstation that contains the destination compartment, identified by its Id.
    /// </summary>
    public Workstation Workstation { get; set; }
}