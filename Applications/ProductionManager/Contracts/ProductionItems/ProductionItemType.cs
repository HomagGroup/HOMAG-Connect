using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(Unknown))]
    Unknown,

    /// <summary />
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(Group))]
    Group,

    /// <summary />
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(Position))]
    Position,

    /// <summary />
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(AssemblyGroup))]
    AssemblyGroup,

    /// <summary />
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(Part))]
    Part,

    /// <summary />
    [Obsolete("Replace with Position")]
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(OrderItem))]
    OrderItem,

    /// <summary />
    [Obsolete("Replace with AssemblyGroup")]
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(AssemblyUnit))]       
    AssemblyUnit,

    /// <summary />
    [Obsolete("Replace with Part")]
    [Display(ResourceType = typeof(ProductionItemTypeDisplayNames), Name = nameof(ProductionOrder))]
    ProductionOrder,    

    /// <summary>
    /// Resource
    /// </summary>
        
    Resource
}