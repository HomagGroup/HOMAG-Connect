using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Type of a production item in the bill of materials of an order. Serialized as the member name and used to select the concrete item contract.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
[ResourceManager(typeof(ProductionItemTypeDisplayNames))]

public enum ProductionItemType
{
    /// <summary>
    /// The type is not known. Used as a fallback when a value cannot be mapped.
    /// </summary>
    Unknown,

    /// <summary>A group that contains other production items.</summary>
    Group,

    /// <summary>A position (order item) of the order, for example a piece of furniture.</summary>
    Position,

    /// <summary>An assembly group that combines parts into an assembly.</summary>
    AssemblyGroup,

    /// <summary>A single part that is produced, for example a panel.</summary>
    Part,

    /// <summary>Obsolete name for <see cref="Position" />.</summary>
    [Obsolete("Replace with Position")]
    OrderItem,

    /// <summary>Obsolete name for <see cref="AssemblyGroup" />.</summary>
    [Obsolete("Replace with AssemblyGroup")]
    AssemblyUnit,

    /// <summary>Obsolete name for <see cref="Part" />.</summary>
    [Obsolete("Replace with Part")]
    ProductionOrder,    

    /// <summary>
    /// A resource used in production, for example hardware or a fitting.
    /// </summary>
        
    Resource
}