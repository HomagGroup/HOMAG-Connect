using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed dividing part in a production protocol, including optimization, pattern,
/// and board-related information.
/// </summary>
/// <example>
/// {
///   "type": "ProcessedPartDividing",
///   "identifier": "10",
///   "description": "Side Panel Left",
///   "boardEntityId": "BOARD-0001",
///   "boardCode": "P2_White_19.0_2800_2070",
///   "optimizationName": "Kitchen_4711_Run_1",
///   "patternCycle": 2,
///   "patternName": "Pattern A"
/// }
/// </example>
public class ProcessedPartDividing : ProcessedPart, ISupportsLocalizedSerialization
{
    /// <inheritdoc />
    public override ProcessedItemType Type
    {
        get
        {
            return ProcessedItemType.ProcessedPartDividing;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }

    /// <summary>
    /// Gets or sets the identifier of the board entity used for the processed part.
    /// </summary>
    /// <example>BOARD-0001</example>
    [JsonProperty(Order = 31)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(BoardEntityId))]
    public string? BoardEntityId { get; set; }

    /// <summary>
    /// Gets or sets the board code.
    /// </summary>
    /// <example>P2_White_19.0_2800_2070</example>
    [JsonProperty(Order = 32)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(BoardCode))]
    public string? BoardCode { get; set; }

    /// <summary>
    /// Gets or sets the optimization name.
    /// </summary>
    /// <example>Kitchen_4711_Run_1</example>
    [JsonProperty(Order = 33)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(OptimizationName))]
    public string? OptimizationName { get; set; }

    /// <summary>
    /// Gets or sets the pattern cycle.
    /// </summary>
    /// <example>2</example>
    [JsonProperty(Order = 34)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(PatternCycle))]
    public int? PatternCycle { get; set; }

    /// <summary>
    /// Gets or sets the pattern name.
    /// </summary>
    /// <example>Pattern A</example>
    [JsonProperty(Order = 3)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(PatternName))]
    public string? PatternName { get; set; }
}