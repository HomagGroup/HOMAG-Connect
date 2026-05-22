using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocolFlow;

/// <summary>
/// Workstation
/// </summary>
public class ProductionProtocolFlowNode : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 20)]
    public Workstation? InputWorkstation { get; set; }

    /// <summary>
    /// Gets or sets the distribution of parts per type on this workstation
    /// </summary>
    [JsonProperty(Order = 21)]
    public Dictionary<ProductionItemType, int> ItemTypeSummary { get; set; } = new();

    /// <summary>
    /// Edges are only prepared for future development, when the flow of each single instance is known
    /// </summary>
    [JsonProperty(Order = 22)]
    public IEnumerable<ProductionProtocolFlowEdge>? OutputWorkstations { get; set; }

}