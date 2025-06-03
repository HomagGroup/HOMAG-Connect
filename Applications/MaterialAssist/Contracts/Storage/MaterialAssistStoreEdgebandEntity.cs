using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Storage;

public class MaterialAssistStoreEdgebandEntity
{
    /// <summary>
    /// Gets or sets the identifier of the edgeband entity.
    /// </summary>
    [StringLength(50)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the length of the edgeband. The unit depends on the settings of the subscription (metric: m, imperial:
    /// ft).
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0.1, 9999.99)]
    public double Length { get; set; }

    /// <summary>
    /// Gets or sets the storage location of the edgeband entity.
    /// </summary>
    public StorageLocation StorageLocation { get; set; }

    /// <summary>
    /// Gets or sets the workstation to which the storage location belongs.
    /// </summary>
    public Workstation Workstation { get; set; }
}