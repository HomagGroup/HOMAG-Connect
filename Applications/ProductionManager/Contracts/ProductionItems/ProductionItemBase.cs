using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents a base class for production items, which can be parts, positions, assembly groups, or resources.
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
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
    /// Grouping
    /// </summary>
    public string? Grouping { get; set; }

    #region (10) Article

    /// <summary>
    /// Gets or sets the type of the production entity.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual ProductionItemType Type { get; set; }

    #endregion

    #region (20) Production

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Gets or sets the procurement type.
    /// </summary>
    [JsonProperty(Order = 3)]
    public string? ProcurementType { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 14)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 15)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Width { get; set; }

    /// <summary>
    /// Barcode used to identify a production entity.
    /// </summary>
    [JsonProperty(Order = 21)]
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the status of the production entity.
    /// </summary>
    [JsonProperty(Order = 22)]
    public ProductionItemStatus ProductionStatus { get; set; } = ProductionItemStatus.New;

    /// <summary>
    /// Gets or sets the quantity of the production entity.
    /// </summary>
    [JsonProperty(Order = 23)]
    public int Quantity { get; set; } = 1;

    #endregion

    #region (30) Production data

    /// <summary>
    /// Gets or sets the planned end date of the production entity.
    /// </summary>
    [JsonProperty(Order = 30)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the started at date of the production entity.
    /// </summary>
    [JsonProperty(Order = 31)]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>Gets or sets the completed date planned</summary>
    [JsonProperty(Order = 32)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the completed at date of the production entity.
    /// </summary>
    [JsonProperty(Order = 33)]
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned delivery date of the production entity.
    /// </summary>
    [JsonProperty(Order = 34)]
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    #endregion

    #region (80) Additional data

    /// <summary>
    /// Gets or sets the notes of the production entity.
    /// </summary>
    [JsonProperty(Order = 80)]
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    [JsonProperty(Order = 81)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    #endregion
}