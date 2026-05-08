using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Classifies the category of a processed item entry in the production protocol.
/// Indicates whether the processing was planned, triggered by rework, or reported beyond the expected quantity.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(ProcessedItemCategoryDisplayNames))]
public enum ProcessedItemCategory
{
    /// <summary>
    /// The part was processed as planned within the expected order quantity.
    /// </summary>
    Planned = 0,

    /// <summary>
    /// The part was re-processed due to a tracked rework.
    /// </summary>
    Rework = 1,

    /// <summary>
    /// The feedback system reported processing beyond the expected quantity
    /// with no corresponding rework entry. This may indicate an unregistered rework,
    /// duplicate scan, or manual interference at the machine or workstation.
    /// </summary>
    Unplanned = 2,
}

