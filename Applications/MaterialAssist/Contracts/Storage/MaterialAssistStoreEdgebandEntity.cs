using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Storage;

/// <summary>
/// Destination and measured length for storing an existing edgeband entity.
/// </summary>
/// <example>
/// {
/// "id": "edge-001",
/// "length": 50,
/// "storageLocation": { "locationId": "rack-a-01" },
/// "workstation": { "id": "b3f05ef4-f34a-4f90-bd7f-7c54805c7c25" }
/// }
/// </example>
public class MaterialAssistStoreEdgebandEntity
{
    /// <summary>
    /// Identifier of the existing edgeband entity to store, not its edgeband type code.
    /// </summary>
    /// <example>edge-001</example>
    [StringLength(50)]
    public string Id { get; set; }

    /// <summary>
    /// Measured length of one edgeband item, applied when storing a Single-managed entity.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m; for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 2000.00)]
    public double Length { get; set; }

    /// <summary>
    /// Destination compartment for the edgeband, identified by its LocationId.
    /// </summary>
    public StorageLocation StorageLocation { get; set; }

    /// <summary>
    /// Workstation that contains the destination compartment, identified by its Id.
    /// </summary>
    public Workstation Workstation { get; set; }
}