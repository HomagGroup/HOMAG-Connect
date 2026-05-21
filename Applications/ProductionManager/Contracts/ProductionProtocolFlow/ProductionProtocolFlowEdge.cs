using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocolFlow;

/// <summary>
/// 
/// </summary>
public class ProductionProtocolFlowEdge : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Data of one Workstation, that gets data from the current Workstation
    /// </summary>
    [JsonProperty(Order = 20)]
    public Workstation? OutputWorkstation { get; set; }

    /// <summary>
    /// Distribution of parts per type travelling on this edge
    /// </summary>
    [JsonProperty(Order = 2)]
    public Dictionary<ProductionItemType, int> ItemTypeSummary { get; set; } = new();
}