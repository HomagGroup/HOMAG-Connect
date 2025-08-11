using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part in a production protocol, including its id, description and quantity processed.
/// </summary>
public class ProcessedPart : ProcessedItem, IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 14)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the material code.
    /// </summary>
    public string? MaterialCode { get; set; }

    /// <summary>
    /// Gets or sets the quantity processed.
    /// </summary>
    [JsonProperty(Order = 23)]
    public int? Quantity { get; set; }

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