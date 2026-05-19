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
    /// 
    /// </summary>
    /// <param name="itemTypeSummary"></param>
    public ProductionProtocolFlowNode(Dictionary<ProductionItemType, int> itemTypeSummary)
    {
        ItemTypeSummary = itemTypeSummary;
    }

    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 20)]
    public Workstation? InputWorkstation { get; set; }

    /// <summary>
    /// Gets or sets the display name
    /// </summary>
    [JsonProperty(Order = 21)]
    public Dictionary<ProductionItemType, int> ItemTypeSummary { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty(Order = 22)]
    public IEnumerable<ProductionProtocolFlowEdge>? OutputWorkstations { get; set; }

}