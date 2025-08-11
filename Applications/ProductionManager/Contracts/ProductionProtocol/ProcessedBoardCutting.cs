using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed board in a production protocol, including its material code, dimensions and unit system.
/// </summary>
public class ProcessedBoardCutting : ProcessedItem, IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    public string? BoardCode { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <inheritdoc />
    public override MachineType MachineType { get; set; } = MachineType.Cutting;

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    public string? MaterialCode { get; set; }

    /// <summary>
    /// Gets or sets number of boards together.
    /// </summary>
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 16)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <inheritdoc />
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}