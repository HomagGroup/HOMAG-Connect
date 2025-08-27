using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Storage;

public class MaterialAssistStoreBoardEntity
{
    /// <summary>
    /// Gets or sets the identifier of the edgeband entity.
    /// </summary>
    [StringLength(50)]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the length. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 9999.99)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the storage location of the edgeband entity.
    /// </summary>
    public StorageLocation StorageLocation { get; set; }

    /// <summary>
    /// Gets or sets the width. The unit depends on the settings of the subscription (metric: mm, imperial: inch).
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 9999.99)]
    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the workstation to which the storage location belongs.
    /// </summary>
    public Workstation Workstation { get; set; }
}