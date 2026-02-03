using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed part in a production protocol, including its id, description and quantity processed.
/// </summary>
public class ProcessedPart : ProcessedOrderItem, IDimensionProperties, IMaterialProperties
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
    [JsonProperty(Order = 21)]
    
    public double? Length { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 22)]
    public double? Width { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 23)]
    public double? Thickness { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 24)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion

    #region IMaterialProperties

    /// <inheritdoc />
    [JsonProperty(Order = 25)]
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [JsonProperty(Order = 26)]
    public Grain Grain { get; set; }

    #endregion
}