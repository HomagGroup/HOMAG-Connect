using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict;

/// <summary>
/// Represents a part input used for cutting prediction requests.
/// </summary>
/// <example>
/// { "id": "Part-1001", "quantity": 2, "length": 1200.0, "width": 450.0, "thickness": 19.0, "unitSystem": "Metric" }
/// </example>
public class CuttingPredictionPart : IDimensionProperties, IContainsUnitSystemDependentProperties
{
    /// <summary>
    /// Gets or sets the client-side identifier of the part.
    /// </summary>
    /// <example>Part-1001</example>
    [JsonProperty(Order = 1)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets how many times the part is required in the prediction request.
    /// </summary>
    /// <example>2</example>
    [Required]
    [JsonProperty(Order = 2)]
    [Range(1, 10000)]
    public int Quantity { get; set; } = 1;

    #region IDimensionsProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 20)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 21)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 23)]
    [Range(0.1, 500)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; }

    #endregion

    #region IContainsUnitSystemDependentProperties Members

    /// <summary>
    /// Gets or sets the unit system for unit-dependent values such as <see cref="Length" />, <see cref="Width" />, and <see cref="Thickness" />.
    /// Use <c>Metric</c> when dimension values are provided in millimeters.
    /// Use <c>Imperial</c> when dimension values are provided in inches.
    /// </summary>
    /// <example>Metric</example>
    [JsonProperty(Order = 999)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion

    /// <summary>
    /// Converts a production part to a cutting prediction part.
    /// </summary>
    /// <param name="productionOrder">The production part to convert.</param>
    public static implicit operator CuttingPredictionPart(Part productionOrder)
    {
        var part = new CuttingPredictionPart
        {
            Id = productionOrder.Id,
            Quantity = productionOrder.Quantity,

            Length = productionOrder.Length,
            Width = productionOrder.Width,
            Thickness = productionOrder.Thickness,

            UnitSystem = productionOrder.UnitSystem
        };

        return part;
    }
}