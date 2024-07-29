using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity type enumeration
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ProductionEntityType
{
    /// <summary>
    /// Fallback
    /// </summary>
    Unknown,

    /// <summary>
    /// Order item
    /// </summary>
    OrderItem,

    /// <summary>
    /// Assembly unit
    /// </summary>
    AssemblyUnit,

    /// <summary>
    /// Production order
    /// </summary>
    ProductionOrder,

    /// <summary>
    /// Resource
    /// </summary>
    Resource
}