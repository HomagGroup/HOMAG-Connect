using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Request object to create an edgeband in materialManager.
/// </summary>
public class MaterialManagerUpdateEdgebandType : MaterialManagerUpdateMaterialType
{
    /// <summary>
    /// Gets or sets the airtec. The unit depends on the settings of the subscription (metric: bar, imperial: psi).
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Bar)]
    [Range(0, double.PositiveInfinity)]
    public double? Airtec { get; set; }

    /// <summary>
    /// Gets or sets the decor embossing code.
    /// </summary>
    [StringLength(50)]
    public string? DecorEmbossingCode { get; set; }

    /// <summary>
    /// Gets or sets the length of the edgeband. The unit depends on the settings of the subscription (metric: m, imperial:
    /// ft).
    /// </summary>
    [Range(0.1, 9999.9)]
    public double? DefaultLength { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code
    /// </summary>
    [StringLength(50, MinimumLength = 1)]
    public string? EdgebandCode { get; set; }

    /// <summary>
    /// Gets or sets the protection layer thickness.
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0, double.PositiveInfinity)]
    public double? FunctionLayerThickness { get; set; }

    /// <summary>
    /// Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial:
    /// inch).
    /// </summary>
    [Range(0.1, 999.9)]
    public double? Height { get; set; }

    /// <summary>
    /// Gets or sets the lasertec (J/cm^2).
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? Lasertec { get; set; }

    /// <summary>
    /// Gets or sets the technology macro name by tapio machine id as the key.
    /// </summary>
    public IDictionary<string, string>? MachineTechnologyMacro { get; set; }

    /// <summary>
    /// Gets or sets the material category.
    /// </summary>
    public EdgebandMaterialCategory? MaterialCategory { get; set; }

    /// <summary>
    /// Gets or sets the process.
    /// </summary>
    public EdgebandingProcess? Process { get; set; }

    /// <summary>
    /// Gets or sets the protection film thickness.
    /// </summary>
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter, 2, 3)]
    [Range(0, double.PositiveInfinity)]
    public double? ProtectionFilmThickness { get; set; }

    /// <summary>
    /// Gets or sets the technology macro name.
    /// </summary>
    [StringLength(50)]
    public string? TechnologyMacro { get; set; }

    /// <summary>
    /// Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial:
    /// inch).
    /// </summary>
    [Range(0.1, 99.9)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the total length available warning limit. The unit depends on the settings of the subscription (metric: m,
    /// imperial: ft).
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? TotalLengthAvailableWarningLimit { get; set; }

    /// <summary>
    /// Gets or sets the total quantity available warning limit.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int? TotalQuantityAvailableWarningLimit { get; set; }
}
