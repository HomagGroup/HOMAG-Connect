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
    public Workstation? Workstation { get; set; }

    /// <summary>
    /// Gets or sets the distribution of parts per type on this workstation
    /// </summary>
    [JsonProperty(Order = 21)]
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Gets or sets the distribution of parts per type on this workstation
    /// </summary>
    [JsonProperty(Order = 23)]
    public IEnumerable<KeyValuePair<ProductionItemType, int>> ItemTypeSummary { get; set; } = new List<KeyValuePair<ProductionItemType, int>>();

}