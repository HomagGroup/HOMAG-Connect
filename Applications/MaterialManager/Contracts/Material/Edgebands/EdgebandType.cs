using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands;

/// <summary>
/// Describes an edgeband material, its dimensions, and the processing details used to identify and apply it.
/// <see href="https://docs.homag.cloud/docs/materialmanager-materialtypen-kantenbaender">Learn more about edgeband types</see>.
/// </summary>
/// <remarks>
/// The product examples use EGGER ABS edging in H2033 ST10 Hunton Oak dark, with a height of 23 mm,
/// a thickness of 2 mm, and a coil length of 75 m.
/// </remarks>
/// <example>
/// { "edgebandCode": "ABS_Hunton_Oak_dark_2.00_23.0_HM", "height": 23.0, "thickness": 2.0, "defaultLength": 75.0,
/// "materialCategory": "ABS", "process": "HotmeltGlue", "manufacturerName": "Egger",
/// "decorName": "Hunton Oak dark", "productName": "EGGER ABS Edging", "decorCode": "H2033",
/// "decorEmbossingCode": "10", "articleNumber": "1380463", "gtin": "9010389217822", "unitSystem": "Metric" }
/// </example>
[DebuggerDisplay("{EdgebandCode}")]
public class EdgebandType : IExtensibleDataObject, IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Date and time when this edgeband type was most recently used, if known.
    /// </summary>
    public DateTimeOffset? LastUsed { get; set; }

    #region IExtensibleDataObject Members

    /// <inheritdoc />
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion

    #region IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion

    #region EdgebandType

    /// <summary>
    /// Unique code for this edgeband type, distinct from the manufacturer's article number.
    /// <para>
    /// A common convention is <c>{material abbreviation}_{decor name}_{thickness}_{height}_{process abbreviation}</c>,
    /// with spaces in the decor name replaced by underscores, for example <c>ABS_Hunton_Oak_dark_2.00_23.0_HM</c>.
    /// In this metric example, thickness and height are in mm and <c>HM</c> represents <see cref="EdgebandingProcess.HotmeltGlue" />.
    /// The code suggested when adding a catalog item can be changed; this naming convention is not a validation rule.
    /// </para>
    /// </summary>
    /// <example>ABS_Hunton_Oak_dark_2.00_23.0_HM</example>
    [Key]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgebandCode { get; set; }

    /// <summary>
    /// Identifier assigned to this edgeband type by an external system.
    /// </summary>
    public string? ExternalSystemId { get; set; }

    /// <summary>
    /// Height of the edgeband strip.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>23.0</example>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    [Range(0.1, 999.9)]
    public double? Height { get; set; }

    /// <summary>
    /// Thickness of the edgeband material.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    /// <example>2.0</example>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0.01, 99.99)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Default length used when storing a new coil of this edgeband type.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    /// <example>75.0</example>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 2000.0)]
    public double? DefaultLength { get; set; }

    /// <summary>
    /// Cost associated with the edgeband.
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? Costs { get; set; }

    #endregion

    #region Material

    /// <summary>
    /// Material category that identifies what the edgeband is made from.
    /// </summary>
    /// <example>ABS</example>
    public EdgebandMaterialCategory? MaterialCategory { get; set; }

    /// <summary>
    /// Process used to attach the edgeband to a board.
    /// </summary>
    /// <example>HotmeltGlue</example>
    public EdgebandingProcess? Process { get; set; }

    /// <summary>
    /// LaserTec process energy density, in J/cm².
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? Lasertec { get; set; } // TODO: Bool? No bool, here the pressure (J/cm^2) for the values is set

    /// <summary>
    /// AirTec process pressure.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: bar.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: psi.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Bar)]
    [Range(0, double.PositiveInfinity)]
    public double? Airtec { get; set; } // TODO: Bool? No bool, here the pressure (bar) for the values is set

    /// <summary>
    /// Thickness of the protective film on the edgeband.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0, double.PositiveInfinity)]
    public double? ProtectionFilmThickness { get; set; }

    /// <summary>
    /// Thickness of the functional layer on the edgeband.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: inch.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0, double.PositiveInfinity)]
    public double? FunctionLayerThickness { get; set; }

    /// <summary>
    /// Name of the technology macro used to process the edgeband.
    /// </summary>
    [StringLength(50)]
    public string? TechnologyMacro { get; set; }

    /// <summary>
    /// Technology macro names associated with machine identifiers.
    /// </summary>
    public IDictionary<string, string>? MachineTechnologyMacro { get; set; }

    #endregion

    #region Manufacturer

    /// <summary>
    /// Name of the company that manufactures the edgeband.
    /// </summary>
    /// <example>Egger</example>
    [StringLength(50)]
    public string? ManufacturerName { get; set; }

    /// <summary>
    /// Name of the decorative design on the edgeband.
    /// </summary>
    /// <example>Hunton Oak dark</example>
    [StringLength(50)]
    public string? DecorName { get; set; }

    /// <summary>
    /// Product name used by the manufacturer or supplier.
    /// </summary>
    /// <example>EGGER ABS Edging</example>
    [StringLength(200)]
    public string? ProductName { get; set; }

    /// <summary>
    /// Code identifying the decorative design.
    /// </summary>
    /// <example>H2033</example>
    [StringLength(50)]
    public string? DecorCode { get; set; }

    /// <summary>
    /// Identifier associated with the decorative design.
    /// </summary>
    [StringLength(50)]
    public string? DecorId { get; set; }

    /// <summary>
    /// Code identifying the embossing or texture pattern.
    /// </summary>
    /// <example>10</example>
    [StringLength(50)]
    public string? DecorEmbossingCode { get; set; }

    /// <summary>
    /// Manufacturer's article number for identifying or ordering the edgeband.
    /// </summary>
    /// <example>1380463</example>
    [StringLength(50)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Global Trade Item Number used to identify the edgeband in the supply chain.
    /// </summary>
    /// <example>9010389217822</example>
    [StringLength(50)]
    public string? Gtin { get; set; }

    /// <summary>
    /// Legacy identifier assigned by an external system. Use <see cref="ExternalSystemId" /> instead.
    /// </summary>
    [Obsolete("use ExternalSystemId instead", false)]
    public string? ExternalId { get; set; }

    #endregion

    #region Inventory

    /// <summary>
    /// Optional quantity threshold for a low-stock warning.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? TotalQuantityAvailableWarningLimit { get; set; }

    /// <summary>
    /// Optional length threshold for a low-stock warning.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    [Range(0, 999999.99)]
    public double? TotalLengthAvailableWarningLimit { get; set; }

    /// <summary>
    /// Quantity of this edgeband type recorded as available in inventory.
    /// </summary>
    public int? TotalQuantityAvailable { get; set; }

    /// <summary>
    /// Total edgeband length recorded as available in inventory.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: m.</para>
    /// <para>Unit for <see cref="UnitSystem.Imperial" />: ft.</para>
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Meter)]
    public double? TotalLengthAvailable { get; set; }

    /// <summary>
    /// Indicates whether the available inventory is below a configured quantity or length warning threshold. A null value
    /// means the status is unknown.
    /// </summary>
    public bool? InsufficientInventory { get; set; }

    #endregion

    #region Additional data

    /// <summary>
    /// Additional notes about this edgeband type.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; }

    /// <summary>
    /// URI of an image that helps identify this edgeband type.
    /// </summary>
    [Obsolete("use PrimaryImage.Thumbnail instead", false)]
    public Uri? Thumbnail { get; set; }

    /// <summary>
    /// Links to the main image that represents this edgeband type, in several sizes. When the edgeband type has a valid
    /// <see cref="DecorId" />, the decor image is used; otherwise the image is selected from the edgeband type's
    /// additional data. The value is null when no image is available.
    /// </summary>
    public ImageUris? PrimaryImage { get; set; }

    #endregion
}