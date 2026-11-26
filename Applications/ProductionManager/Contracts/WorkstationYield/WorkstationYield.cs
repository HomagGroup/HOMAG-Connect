using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.WorkstationYield;

/// <summary>
/// Represents the number of parts processed at a workstation, in total and per production item type.
/// </summary>
/// <example>
/// { "totalCount": 12, "yields": [ { "key": "Part", "value": 12 } ] }
/// </example>
public class WorkstationYield : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Workstation the yield was measured at. Optional.
    /// </summary>
    [JsonProperty(Order = 20)]
    [Display(ResourceType = typeof(WorkstationYieldPropertyNames), Name = nameof(Workstation))]
    public Workstation? Workstation { get; set; }

    /// <summary>
    /// Total number of parts processed at this workstation.
    /// </summary>
    [JsonProperty(Order = 21)]
    [Display(ResourceType = typeof(WorkstationYieldPropertyNames), Name = nameof(TotalCount))]
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Number of processed parts per production item type at this workstation, as a list of key/value entries. Empty if nothing was processed.
    /// </summary>
    [JsonProperty(Order = 23)]
    [Display(ResourceType = typeof(WorkstationYieldPropertyNames), Name = nameof(Yields))]
    public IEnumerable<KeyValuePair<ProductionItemType, int>> Yields { get; set; } = new List<KeyValuePair<ProductionItemType, int>>();

}
