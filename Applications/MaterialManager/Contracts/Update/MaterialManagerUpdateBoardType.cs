using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Provides board-type values for an update. The default client serializer omits properties whose values are null.
/// </summary>
/// <example>
/// { "boardCode": "BOARD-001", "thickness": 19.0, "width": 2070.0 }
/// </example>
public class MaterialManagerUpdateBoardType : MaterialManagerUpdateMaterialType
{
    /// <summary>
    /// Code that identifies this board format or variant.
    /// </summary>
    [StringLength(50, MinimumLength = 1)]
    public string? BoardCode { get; set; } = null;

    /// <summary>
    /// Surface coating category of the board.
    /// </summary>
    public CoatingCategory? CoatingCategory { get; set; } = null;

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
    public Grain? Grain { get; set; } = null;

    /// <summary>
    /// Length of the board.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Range(0.1, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; } = null;

    /// <summary>
    /// Whether configuration changes to this board type are locked.
    /// </summary>
    public bool? LockedForConfiguration { get; set; } = null;

    /// <summary>
    /// Whether this board type is excluded from optimization.
    /// </summary>
    public bool? LockedForOptimization { get; set; } = null;

    /// <summary>
    /// Base material the board is made from.
    /// </summary>
    public BoardMaterialCategory? MaterialCategory { get; set; } = null;

    /// <summary>
    /// Code identifying the shared material associated with this board type.
    /// </summary>
    [StringLength(50, MinimumLength = 1)]
    public string? MaterialCode { get; set; } = null;

    /// <summary>
    /// Whether optimization treats this board type as having unlimited stock.
    /// </summary>
    public bool? OptimizeAgainstInfinite { get; set; } = null;

    /// <summary>
    /// Standard cut quality assigned to the board.
    /// </summary>
    public StandardQuality? StandardQuality { get; set; } = null;

    /// <summary>
    /// Thickness of the board.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Range(0.1, 999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; } = null;

    /// <summary>
    /// Quantity threshold for a low-stock warning.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? TotalQuantityAvailableWarningLimit { get; set; } = null;

    /// <summary>
    /// Identifies whether this entry represents a full board, an offcut, or a template.
    /// </summary>
    public BoardTypeType? Type { get; set; } = null;

    /// <summary>
    /// Width of the board.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Range(0.1, 19999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; } = null;
}