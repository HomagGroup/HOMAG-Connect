using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed item entry in a production protocol.
/// Serves as the base contract for processed parts, positions, and assembly groups.
/// </summary>
/// <example>
/// {
///   "type": "ProcessedPart",
///   "workstationId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff",
///   "from": "productionAssist Cutting",
///   "subscriptionId": "11111111-2222-3333-4444-555555555555",
///   "timestamp": "2025-01-20T14:32:10+00:00",
///   "itemType": "Part"
/// }
/// </example>
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
    /// Gets or sets additional custom properties.
    /// Any JSON properties not mapped to typed members can be captured here via <c>[JsonExtensionData]</c>.
    /// </summary>
    /// <example>{ "machine": "SAWTEQ B-300" }</example>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the source of the processing information, for example the workstation or system name.
    /// </summary>
    /// <example>productionAssist Cutting</example>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(From))]
    public string? From { get; set; }

    /// <summary>
    /// Gets or sets the production item type.
    /// The base implementation returns <c>Unknown</c>; derived types provide the concrete value.
    /// </summary>
    /// <example>Part</example>
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
    /// <example>11111111-2222-3333-4444-555555555555</example>
    [JsonProperty(Order = 3)]
    [Display(AutoGenerateField = false)]
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the item was processed.
    /// </summary>
    /// <example>2025-01-20T14:32:10+00:00</example>
    [JsonProperty(Order = 4)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Timestamp))]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Gets or sets the processed item discriminator used during JSON deserialization to determine the concrete contract type.
    /// </summary>
    /// <example>ProcessedPart</example>
    [JsonProperty(Order = 0)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Type))]
    public virtual ProcessedItemType Type { get; set; } = ProcessedItemType.ProcessedItem;

    /// <summary>
    /// Gets or sets the unique identifier of the workstation where the item was processed.
    /// </summary>
    /// <example>6f9619ff-8b86-d011-b42d-00cf4fc964ff</example>
    [JsonProperty(Order = 1)]
    [Display(AutoGenerateField = false)]
    public Guid WorkstationId { get; set; }

    /// <summary>
    /// The processed part quality 
    /// </summary>
    [JsonProperty(Order = 6)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Quality))]
    public ProcessedItemQuality Quality { get; set; } = ProcessedItemQuality.Good;
}