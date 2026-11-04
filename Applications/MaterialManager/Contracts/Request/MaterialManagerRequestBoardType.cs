using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Request;

/// <summary>
/// Describes the shared material and dimensions of a board type to be created.
/// A board code identifies the specific format, while the material code identifies its shared material.
/// </summary>
/// <example>
/// { "boardCode": "BOARD-001", "materialCode": "MAT-001", "coatingCategory": "MelamineThermoset",
/// "grain": "Lengthwise", "length": 2800.0, "materialCategory": "Chipboard", "thickness": 19.0,
/// "type": "Board", "width": 2070.0 }
/// </example>
public class MaterialManagerRequestBoardType : MaterialManagerRequestMaterialType
{
    /// <summary>
    /// Code that identifies this board format or variant.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string BoardCode { get; set; }

    /// <summary>
    /// Surface coating category of the board.
    /// </summary>
    [Required]
    public CoatingCategory CoatingCategory { get; set; } = CoatingCategory.Undefined;

    /// <summary>
    /// Mass per unit volume of the board material.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: kg/m³.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: lb/ft³.</para>
    /// </summary>
    [Range(0.1, double.PositiveInfinity)]
    [ValueDependsOnUnitSystem(BaseUnit.KilogramPerCubicMeter)]
    public double? Density { get; set; }

    /// <summary>
    /// Embossing or texture pattern on the bottom decor side.
    /// </summary>
    [StringLength(50)]
    public string? EmbossingBottom { get; set; }

    /// <summary>
    /// Embossing or texture pattern on the top decor side.
    /// </summary>
    [StringLength(50)]
    public string? EmbossingTop { get; set; }

    /// <summary>
    /// Identifier used by an external system to refer to this board type.
    /// </summary>
    public string? ExternalSystemId { get; set; }

    /// <summary>
    /// Grain direction, which guides how parts can be oriented during optimization.
    /// </summary>
    [Required]
    public Grain Grain { get; set; }

    /// <summary>
    /// Length of the board.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Required]
    [Range(0.1, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Length { get; set; }

    /// <summary>
    /// Whether configuration changes to this board type are locked.
    /// </summary>
    public bool LockedForConfiguration { get; set; }

    /// <summary>
    /// Whether this board type is excluded from optimization.
    /// </summary>
    public bool LockedForOptimization { get; set; }

    /// <summary>
    /// Base material the board is made from.
    /// </summary>
    [Required]
    public BoardMaterialCategory MaterialCategory { get; set; } = BoardMaterialCategory.Undefined;

    /// <summary>
    /// Code identifying the shared material associated with this board type.
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string MaterialCode { get; set; }

    /// <summary>
    /// Whether optimization treats this board type as having unlimited stock.
    /// </summary>
    public bool OptimizeAgainstInfinite { get; set; } = true;

    /// <summary>
    /// Standard cut quality assigned to the board.
    /// </summary>
    public StandardQuality StandardQuality { get; set; } = StandardQuality.FinishCut;

    /// <summary>
    /// Thickness of the board.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Required]
    [Range(0.1, 999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Thickness { get; set; }

    /// <summary>
    /// Optional quantity threshold for a low-stock warning.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? TotalQuantityAvailableWarningLimit { get; set; }

    /// <summary>
    /// Identifies whether this entry represents a full board, an offcut, or a template.
    /// </summary>
    [Required]
    public BoardTypeType Type { get; set; } = BoardTypeType.Board;

    /// <summary>
    /// Width of the board.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Required]
    [Range(0.1, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double Width { get; set; }
}