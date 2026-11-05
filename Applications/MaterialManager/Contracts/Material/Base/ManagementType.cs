using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Base;

/// <summary>
/// Describes whether a physical inventory entry is tracked individually, as a stack, or as goods in stock.
/// </summary>
[ResourceManager(typeof(ManagementTypeDisplayNames))]
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ManagementType
{
    /// <summary>
    /// Track one physical item as an individual inventory entry.
    /// </summary>
    [Display(Description = "Single")]
    Single,

    /// <summary>
    /// Track a stack of physical items together as one inventory entry.
    /// </summary>
    [Display(Description = "Stack")]
    Stack,

    /// <summary>
    /// Track a quantity of stock material together rather than identifying each item separately.
    /// </summary>
    [Display(Description = "Goods in stock")]
    GoodsInStock
}