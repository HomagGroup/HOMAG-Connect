using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;
using JsonSubTypes;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
[JsonSubtypes.KnownSubType(typeof(ProcessedPartLaminating), ProcessedItemType.ProcessedPartLaminating)]
[JsonSubtypes.KnownSubType(typeof(ProcessedPartCnc), ProcessedItemType.ProcessedPartCnc)]
[JsonSubtypes.KnownSubType(typeof(ProcessedPart), ProcessedItemType.ProcessedPart)]
[JsonSubtypes.KnownSubType(typeof(ProcessedPosition), ProcessedItemType.ProcessedPosition)]
[JsonSubtypes.KnownSubType(typeof(ProcessedAssemblyGroup), ProcessedItemType.ProcessedAssemblyGroup)]
public class ProcessedItem : ISupportsLocalizedSerialization
{
    /// <summary>
    /// Additional properties that are not part of the standard contract. Optional.
    /// <para>Any JSON property not mapped to a typed member is captured here.</para>
    /// </summary>
    /// <example>{ "machine": "SAWTEQ B-300" }</example>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalProperties))]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Source of the processing information, for example the workstation or system name. Optional.
    /// </summary>
    /// <example>productionAssist Cutting</example>
    [JsonProperty(Order = 2)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(From))]
    public string? From { get; set; }

    /// <summary>
    /// Type of the production item that was processed. The value is determined by the concrete processed item type;
    /// a value sent by the consumer is ignored. The base type reports <see cref="ProductionItemType.Unknown" />.
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
    /// Identifier of the subscription the processed item belongs to.
    /// </summary>
    /// <example>11111111-2222-3333-4444-555555555555</example>
    [JsonProperty(Order = 3)]
    [Display(AutoGenerateField = false)]
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Date and time the item was processed.
    /// </summary>
    /// <example>2025-01-20T14:32:10+00:00</example>
    [JsonProperty(Order = 4)]
    [Display(ResourceType = typeof(Resources), Name = nameof(Timestamp))]
    [DisplayFormat(DataFormatString = "{0:g}")]
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Discriminator that selects the concrete processed item contract when deserializing. Defaults to
        /// <see cref="ProcessedItemType.ProcessedItem" />.
    /// </summary>
    /// <example>ProcessedPart</example>
    [JsonProperty(Order = 0)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Type))]
    [DefaultValue(ProcessedItemType.ProcessedItem)]
    public virtual ProcessedItemType Type { get; set; } = ProcessedItemType.ProcessedItem;

    /// <summary>
    /// Unique identifier of the workstation where the item was processed.
    /// </summary>
    /// <example>6f9619ff-8b86-d011-b42d-00cf4fc964ff</example>
    [JsonProperty(Order = 1)]
    [Display(AutoGenerateField = false)]
    public Guid WorkstationId { get; set; }

    /// <summary>
    /// Category of the processing: planned, rework, or unplanned. Defaults to <see cref="ProcessedItemCategory.Planned" />.
    /// </summary>
    /// <example>Planned</example>
    [JsonProperty(Order = 6)]
    [Display(ResourceType = typeof(ProductionProtocolPropertyDisplayNames), Name = nameof(Category))]
    [DefaultValue(ProcessedItemCategory.Planned)]
    public ProcessedItemCategory Category { get; set; } = ProcessedItemCategory.Planned;
    
}
