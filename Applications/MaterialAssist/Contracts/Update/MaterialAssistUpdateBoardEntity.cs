using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Update;

public class MaterialAssistUpdateBoardEntity
{
    /// <summary>
    /// Gets or sets the additional comments.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the edgeband entity.
    /// </summary>
    [StringLength(50)]
    public string? Id { get; set; } = null;

    /// <summary>
    /// Only available for single boards
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.99)]
    public double? Length { get; set; }

    /// <summary>
    /// Only available for stacks or goods in stock
    /// </summary>
    [Range(1, 100)]
    public int? Quantity { get; set; }

    /// <summary>
    /// Only available for single boards
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 19999.99)]
    public double? Width { get; set; }
}