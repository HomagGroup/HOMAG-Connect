using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production status of a production item. Serialized as the member name.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(ProductionItemStatusDisplayNames))]

public enum ProductionItemStatus
{
    /// <summary>
    /// The status could not be determined. Do not use it when creating items.
    /// </summary>
    Unknown,

    /// <summary>
    /// The item exists but has not been released for production yet.
        /// </summary>
    New,

    /// <summary>
    /// The item has been released and is waiting to be produced.
    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// Production of the item has started but is not finished.
    /// </summary>
    InProduction,

    /// <summary>
    /// Production of the item is finished.
    /// </summary>
    Completed,
}