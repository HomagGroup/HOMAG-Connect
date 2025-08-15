using HomagConnect.Base.Contracts.Enumerations;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed item in a production protocol.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartCutting), nameof(ProcessedPartCutting))]
[JsonSubtypes.KnownSubType(typeof(ProcessedBoardCutting), nameof(ProcessedBoardCutting))]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartCnc), nameof(ProcessedPartCnc))]
public class ProcessedItem
{
    /// <summary>
    /// Gets or sets the additional properties associated with the processed item.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the item type of the processed item.
    /// </summary>
    public virtual ProcessedItemType ItemType
    {
        get
        {
            return ProcessedItemType.Unknown;
        }
    }

    /// <summary>
    /// Gets or sets the machine name where the item was processed.
    /// </summary>
    public string? MachineName { get; set; }

    /// <summary>
    /// Gets or sets the machine number where the item was processed.
    /// </summary>
    public string? MachineNumber { get; set; }

    /// <summary>
    /// Gets or sets the machine type where the item was processed.
    /// </summary>
    public virtual MachineType MachineType
    {
        get
        {
            return MachineType.Unknown;
        }
    }

    /// <summary>
    /// Gets or sets the source of the processing information.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the subscription identifier associated with the processed item.
    /// </summary>
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the item was processed.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }

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
}