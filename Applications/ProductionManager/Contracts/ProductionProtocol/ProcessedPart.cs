using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part in a production protocol, including dimensional and material information.
/// </summary>
/// <example>
/// {
///   "identifier": "10",
///   "description": "Side Panel Left",
///   "quantity": 4,
///   "length": 720,
///   "width": 480,
///   "thickness": 19.0,
///   "unitSystem": "Metric",
///   "material": "P2_White_19.0",
///   "grain": "Lengthwise"
/// }
/// </example>
public class ProcessedPart : ProcessedOrderItem, IDimensionProperties, IMaterialProperties, IContainsUnitSystemDependentProperties
{
    /// <inheritdoc />
    public override ProductionItemType ItemType
    {
        get
        {
            return ProductionItemType.Part;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }

    /// <inheritdoc />
    public override ProcessedItemType Type
    {
        get
        {
            return ProcessedItemType.ProcessedPart;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }

    #region IDimensionProperties

    /// <inheritdoc />
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(Length))]
    [JsonProperty(Order = 21)]
    public double? Length { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(Width))]
    [JsonProperty(Order = 22)]
    public double? Width { get; set; }

    /// <inheritdoc />
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(Thickness))]
    [JsonProperty(Order = 23)]
    public double? Thickness { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 24)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(UnitSystem))]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion

    #region IMaterialProperties

    /// <inheritdoc />
    [JsonProperty(Order = 25)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(Material))]
    public string? Material { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 26)]
    [Display(ResourceType = typeof(Base.Contracts.Resources), Name = nameof(Grain))]
    public Grain Grain { get; set; }

    #endregion
}