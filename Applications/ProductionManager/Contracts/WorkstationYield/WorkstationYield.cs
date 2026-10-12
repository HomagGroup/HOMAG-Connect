using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.WorkstationYield;

/// <summary>
/// Workstation
/// </summary>
public class WorkstationYield : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Gets or sets the tapio machine ID
    /// </summary>
    [JsonProperty(Order = 20)]
    [Display(ResourceType = typeof(WorkstationYieldPropertyNames), Name = nameof(Workstation))]
    public Workstation? Workstation { get; set; }

    /// <summary>
    /// Gets or sets the total count of parts on this workstation
    /// </summary>
    [JsonProperty(Order = 21)]
    [Display(ResourceType = typeof(WorkstationYieldPropertyNames), Name = nameof(TotalCount))]
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Gets or sets the distribution of parts per type on this workstation
    /// </summary>
    [JsonProperty(Order = 23)]
    [Display(ResourceType = typeof(WorkstationYieldPropertyNames), Name = nameof(Yields))]
    public IEnumerable<KeyValuePair<ProductionItemType, int>> Yields { get; set; } = new List<KeyValuePair<ProductionItemType, int>>();

}