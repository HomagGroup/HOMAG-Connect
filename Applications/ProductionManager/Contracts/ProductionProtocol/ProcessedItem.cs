using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed item in a production protocol.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartDividing), ProcessedItemType.ProcessedPartDividing)]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartEdgebanding), ProcessedItemType.ProcessedPartEdgebanding)]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartCnc), ProcessedItemType.ProcessedPartCnc)]
[JsonSubtypes.KnownSubType(typeof(ProcessedPart), ProcessedItemType.ProcessedPart)]
[JsonSubtypes.KnownSubType(typeof(ProcessedPosition), ProcessedItemType.ProcessedPosition)]
[JsonSubtypes.KnownSubType(typeof(ProcessedAssemblyGroup), ProcessedItemType.ProcessedAssemblyGroup)]
public class ProcessedItem : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Gets or sets the additional properties associated with the processed item.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the source of the processing information.
    /// </summary>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(From))]
    public string? From { get; set; }

    /// <summary>
    /// Gets or sets the item type of the processed item.
    /// </summary>
    [JsonProperty(Order = 5)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(ItemType))]
    public virtual ProductionItemType ItemType
    {
        get
        {
            return ProductionItemType.Unknown;
        }
        // ReSharper disable once ValueParameterNotUsed
        set
        {
            // Ignored, needed for serialization
        }
    }

    /// <summary>
    /// Gets or sets the subscription identifier associated with the processed item.
    /// </summary>
    [JsonProperty(Order = 3)]
    [Display(AutoGenerateField = false)]
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the item was processed.
    /// </summary>
    [JsonProperty(Order = 4)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Timestamp))]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Gets the type of the processed item, which is a combination of the item type and machine type which is used during
    /// deserialization to determine the right object type.
    /// </summary>
    [JsonProperty(Order = 0)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Type))]
    public virtual ProcessedItemType Type { get; set; } = ProcessedItemType.ProcessedItem;

    /// <summary>
    /// Gets or sets the WorkstationId.
    /// </summary>
    [JsonProperty(Order = 1)]
    [Display(AutoGenerateField = false)]
    public Guid WorkstationId { get; set; }
}