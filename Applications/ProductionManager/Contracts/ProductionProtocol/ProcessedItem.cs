using HomagConnect.ProductionManager.Contracts.ProductionItems;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed item in a production protocol.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartDividing), nameof(ProcessedPartDividing))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartEdgebanding), nameof(ProcessedPartEdgebanding))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartCnc), nameof(ProcessedPartCnc))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPart), nameof(ProcessedPart))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPosition), nameof(ProcessedPosition))]
[JsonSubtypes.KnownSubType(typeof(ProcessedAssemblyGroup), nameof(ProcessedAssemblyGroup))]
public class ProcessedItem
{    
    /// <summary>
    /// Gets the type of the processed item, which is a combination of the item type and machine type which is used during
    /// deserialization to determine the right object type.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual string Type
    {
        get
        {
            return GetType().Name;
        }
    }

    /// <summary>
    /// Gets or sets the WorkstationId.
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid WorkstationId { get; set; }


    /// <summary>
    /// Gets or sets the source of the processing information.
    /// </summary>
    [JsonProperty(Order = 2)]
    public string? From { get; set; }

    /// <summary>
    /// Gets or sets the subscription identifier associated with the processed item.
    /// </summary>
    [JsonProperty(Order = 3)]
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the item was processed.
    /// </summary>
    [JsonProperty(Order = 4)]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the item type of the processed item.
    /// </summary>
    [JsonProperty(Order = 5)]
    public virtual ProductionItemType ItemType
    {
        get
        {
            return ProductionItemType.Unknown;
        }
    }


    /// <summary>
    /// Gets or sets the additional properties associated with the processed item.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}