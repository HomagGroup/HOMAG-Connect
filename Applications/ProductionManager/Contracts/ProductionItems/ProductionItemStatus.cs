using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production item status enumeration
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ProductionItemStatus
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// New
    /// </summary>
    New,

    /// <summary>
    /// Ready for production
    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// In Production
    /// </summary>
    InProduction,

    /// <summary>
    /// Completed
    /// </summary>
    Completed,
}