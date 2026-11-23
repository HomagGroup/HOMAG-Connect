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
    /// Identifier of the board entity the part was cut from. Optional.
    /// </summary>
    /// <example>BOARD-0001</example>
    [JsonProperty(Order = 31)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(BoardEntityId))]
    public string? BoardEntityId { get; set; }

    /// <summary>
    /// Code of the board type the part was cut from. Optional.
    /// </summary>
    /// <example>P2_White_19.0_2800_2070</example>
    [JsonProperty(Order = 32)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(BoardCode))]
    public string? BoardCode { get; set; }

    /// <summary>
    /// Name of the optimization the part was produced in. Optional.
    /// </summary>
    /// <example>Kitchen_4711_Run_1</example>
    [JsonProperty(Order = 33)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(OptimizationName))]
    public string? OptimizationName { get; set; }

    /// <summary>
    /// Number of the cycle of the cutting pattern in which the part was produced. Optional.
    /// </summary>
    /// <example>2</example>
    [JsonProperty(Order = 34)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(PatternCycle))]
    public int? PatternCycle { get; set; }

    /// <summary>
    /// Name of the cutting pattern the part was produced with. Optional.
    /// </summary>
    /// <example>Pattern A</example>
    [JsonProperty(Order = 35)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(PatternName))]
    public string? PatternName { get; set; }
}