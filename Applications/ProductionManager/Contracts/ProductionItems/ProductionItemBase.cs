using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using JsonSubTypes;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Base type of all items in the bill of materials of an order: groups, positions, parts, assembly groups, and resources.
/// The <see cref="Type" /> property determines the concrete item type when deserializing.
/// </summary>
/// <example>
/// { "type": "Part", "id": "P-0001", "identifier": "4711000001", "articleNumber": "SIDE-L", "description": "Left side panel",
/// "length": 720.0, "width": 560.0, "barcode": "4711000001", "productionStatus": "New", "quantity": 1,
/// "startDatePlanned": "2025-04-03T00:00:00+00:00", "completionDatePlanned": "2025-04-04T00:00:00+00:00", "notes": "Handle with care" }
/// </example>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(Group), ProductionItemType.Group)]
[JsonSubtypes.KnownSubType(typeof(Position), ProductionItemType.Position)]
[JsonSubtypes.KnownSubType(typeof(Part), ProductionItemType.Part)]
[JsonSubtypes.KnownSubType(typeof(AssemblyGroup), ProductionItemType.AssemblyGroup)]
[JsonSubtypes.KnownSubType(typeof(Resource), ProductionItemType.Resource)]
[JsonSubtypes.KnownSubType(typeof(Position), ProductionItemType.OrderItem)]
[JsonSubtypes.KnownSubType(typeof(Part), ProductionItemType.ProductionOrder)]
[JsonSubtypes.KnownSubType(typeof(AssemblyGroup), ProductionItemType.AssemblyUnit)]
[DebuggerDisplay("Id={Id}, Number={ArticleNumber}")]
public class ProductionItemBase
{
    /// <summary>
    /// Grouping criterion the item is assigned to. Optional.
    /// </summary>
    [StringLength(100)]
    public string? Grouping { get; set; }

    #region (10) Article

    /// <summary>
    /// Type of the item, which determines the concrete contract the item is deserialized to.
    /// </summary>
    /// <example>Part</example>
    [JsonProperty(Order = 0)]
    public virtual ProductionItemType Type { get; set; }

    #endregion

    #region (20) Production

    /// <summary>
    /// Identifier of the item within the order. Optional.
    /// </summary>
    /// <example>P-0001</example>
    [JsonProperty(Order = 1)]
    public string? Id { get; set; }

    /// <summary>
    /// Identifier to refer to the item: the <see cref="Barcode" /> if one exists, otherwise the <see cref="Id" />.
    /// The value is computed; a value sent by the consumer is ignored.
    /// </summary>
    /// <example>4711000001</example>
    [JsonProperty(Order = 2)]
    public string? Identifier
    {
        get
        {
            return !string.IsNullOrWhiteSpace(Barcode) ? Barcode : Id;
        }
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // Needed for serialization. Do nothing. If removed the property is added to AdditionalProperties on deserialization.
        }
    }

    /// <summary>
    /// Identifier of the order item (position) the item belongs to. Optional.
    /// </summary>
    [JsonProperty(Order = 3)]
    [StringLength(100)]
    public string? OrderItem { get; set; }

    /// <summary>
    /// Group of the order item the item belongs to. Optional.
    /// </summary>
    [JsonProperty(Order = 4)]
    [StringLength(100)]
    public string? OrderItemGroup { get; set; }

    /// <summary>
    /// Article number of the item. Optional.
    /// </summary>
    /// <example>SIDE-L</example>
    [JsonProperty(Order = 5)]
    [StringLength(100)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Description of the item. Optional.
    /// </summary>
    /// <example>Left side panel</example>
    [JsonProperty(Order = 6)]
    [StringLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Article group the item belongs to. Optional.
    /// </summary>
    [JsonProperty(Order = 7)]
    [StringLength(100)]
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// How the item is procured, for example produced in-house or purchased. Optional.
    /// </summary>
    [JsonProperty(Order = 8)]
    [StringLength(100)]
    public string? ProcurementType { get; set; }

    /// <summary>
    /// Identifier of the item in an external system, usable as a reference. Optional.
    /// </summary>
    [JsonProperty(Order = 9)]
    [StringLength(300)]
    public string? ExternalSystemId { get; set; }

    /// <summary>
    /// Length of the item. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    /// <example>720.0</example>
    [JsonProperty(Order = 14)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <summary>
    /// Width of the item. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    /// <example>560.0</example>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <summary>
    /// Barcode that identifies the item in production, for example when scanning at a machine. Optional.
    /// </summary>
    /// <example>4711000001</example>
    [JsonProperty(Order = 21)]
    [StringLength(100)]
    public string? Barcode { get; set; }

    /// <summary>
    /// Current production status of the item. Defaults to <see cref="ProductionItemStatus.New" />.
    /// </summary>
    /// <example>New</example>
    [JsonProperty(Order = 22)]
    [DefaultValue(ProductionItemStatus.New)]
    public ProductionItemStatus ProductionStatus { get; set; } = ProductionItemStatus.New;

    /// <summary>
    /// Number of pieces of the item. Defaults to 1.
    /// </summary>
    /// <example>1</example>
    [JsonProperty(Order = 23)]
    [DefaultValue(1)]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Unit in which <see cref="Quantity" /> is expressed, for example pieces. Optional.
    /// </summary>
    [JsonProperty(Order = 24)]
    [StringLength(50)]
    public string? QuantityUnit { get; set; }

    #endregion

    #region (30) Production data

    /// <summary>
    /// Date on which production of the item is planned to start. Optional.
    /// </summary>
    /// <example>2025-04-03T00:00:00+00:00</example>
    [JsonProperty(Order = 30)]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Date and time production of the item started. Empty until production starts.
    /// </summary>
    /// <example>2025-04-03T07:15:00+00:00</example>
    [JsonProperty(Order = 31)]
    [DisplayFormat(DataFormatString = "{0:g}")]
    [DataType(DataType.DateTime)]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>
        /// Date on which production of the item is planned to be completed. Optional.
        /// </summary>
        /// <example>2025-04-04T00:00:00+00:00</example>
    [JsonProperty(Order = 32)]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Date and time production of the item was completed. Empty until completion.
    /// </summary>
    /// <example>2025-04-04T15:40:00+00:00</example>
    [JsonProperty(Order = 33)]
    [DisplayFormat(DataFormatString = "{0:g}")]
    [DataType(DataType.DateTime)]
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Date on which the item is planned to be delivered. Optional.
    /// </summary>
    /// <example>2025-05-15T00:00:00+00:00</example>
    [JsonProperty(Order = 34)]
    [DisplayFormat(DataFormatString = "{0:d}")]
    [DataType(DataType.Date)]
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    /// <summary>
    /// Manufacturer of the item. Optional.
    /// </summary>
    [JsonProperty(Order = 35)]
    [StringLength(255)]
    public string? Manufacturer { get; set; }

    #endregion

    #region (80) Additional data

    /// <summary>
    /// Free-text notes about the item. Optional.
    /// </summary>
    /// <example>Handle with care</example>
    [JsonProperty(Order = 80)]
    [StringLength(255)]
    public string? Notes { get; set; }

    /// <summary>
    /// Additional data entries attached to the item. Optional.
    /// </summary>
    [JsonProperty(Order = 81)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Additional properties configured in productionManager that are not part of the standard contract. Optional.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    #endregion
}
