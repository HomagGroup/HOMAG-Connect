using System.ComponentModel.DataAnnotations;

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
    [Display(ResourceType = typeof(ProductionItemStatusDisplayNames), Name = nameof(Unknown))]
    Unknown,

    /// <summary>
    /// New
    /// </summary>
    [Display(ResourceType = typeof(ProductionItemStatusDisplayNames), Name = nameof(New))]
    New,

    /// <summary>
    /// Ready for production
    /// </summary>
    [Display(ResourceType = typeof(ProductionItemStatusDisplayNames), Name = nameof(ReadyForProduction))]
    ReadyForProduction,

    /// <summary>
    /// In Production
    /// </summary>
    [Display(ResourceType = typeof(ProductionItemStatusDisplayNames), Name = nameof(InProduction))]
    InProduction,

    /// <summary>
    /// Completed
    /// </summary>
    [Display(ResourceType = typeof(ProductionItemStatusDisplayNames), Name = nameof(Completed))]
    Completed,
}