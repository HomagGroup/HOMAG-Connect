using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity type enumeration
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ProductionItemType
{
    /// <summary>
    /// Fallback
    /// </summary>
    Unknown,

    /// <summary />
    Position,

    /// <summary />
    AssemblyGroup,

    /// <summary />
    Part,

    /// <summary />
    [Obsolete("Replace with Position")]
    OrderItem,

    /// <summary />
    [Obsolete("Replace with AssemblyGroup")]
    AssemblyUnit,

    /// <summary />
    [Obsolete("Replace with Part")]
    ProductionOrder,

    /// <summary>
    /// Resource
    /// </summary>
    Resource
}