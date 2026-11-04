using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Provides edgeband values for an update. The default client serializer omits properties whose values are null.
/// </summary>
/// <example>
/// { "edgebandCode": "EDGE-001", "defaultLength": 100.0, "height": 23.0, "thickness": 0.8 }
/// </example>
public class MaterialManagerUpdateEdgebandType : MaterialManagerUpdateMaterialType
{
    /// <summary>
    /// AirTec process pressure.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: bar.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: psi.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Bar)]
    [Range(0, double.PositiveInfinity)]
    public double? Airtec { get; set; }

    /// <summary>
    /// Code identifying the edgeband's embossing or texture pattern.
    /// </summary>
    [StringLength(50)]
    public string? DecorEmbossingCode { get; set; }

    /// <summary>
    /// Default length of the edgeband roll.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [Range(0, 2000.0)]
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double? DefaultLength { get; set; }

    /// <summary>
    /// Code used to identify this edgeband type.
    /// </summary>
    [StringLength(50, MinimumLength = 1)]
    public string? EdgebandCode { get; set; }

    /// <summary>
    /// Identifier used by an external system to refer to this edgeband type.
    /// </summary>
    public string? ExternalSystemId { get; set; }

    /// <summary>
    /// Thickness of the functional layer on the edgeband.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0, double.PositiveInfinity)]
    public double? FunctionLayerThickness { get; set; }

    /// <summary>
    /// Height of the edgeband strip.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Range(0.1, 999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Height { get; set; }

    /// <summary>
    /// LaserTec process energy density, in J/cm².
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? Lasertec { get; set; }

    /// <summary>
    /// Technology macro names associated with machine identifiers.
    /// </summary>
    public IDictionary<string, string>? MachineTechnologyMacro { get; set; }

    /// <summary>
    /// Material category that identifies what the edgeband is made from.
    /// </summary>
    public EdgebandMaterialCategory? MaterialCategory { get; set; }

    /// <summary>
    /// Process used to attach the edgeband to a board.
    /// </summary>
    public EdgebandingProcess? Process { get; set; }

    /// <summary>
    /// Thickness of the protective film on the edgeband.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0, double.PositiveInfinity)]
    public double? ProtectionFilmThickness { get; set; }

    /// <summary>
    /// Name of the technology macro used to process the edgeband.
    /// </summary>
    [StringLength(50)]
    public string? TechnologyMacro { get; set; }

    /// <summary>
    /// Thickness of the edgeband material.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [Range(0.01, 99.99)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Length threshold for a low-stock warning.
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="HomagConnect.Base.Contracts.Enumerations.UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [Range(0, 999999.99)]
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double? TotalLengthAvailableWarningLimit { get; set; }

    /// <summary>
    /// Quantity threshold for a low-stock warning.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? TotalQuantityAvailableWarningLimit { get; set; }
}