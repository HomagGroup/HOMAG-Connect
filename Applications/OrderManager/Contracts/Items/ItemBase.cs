using System.Collections.ObjectModel;
using System.Diagnostics;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Items;

/// <summary>
/// Production entity
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(OrderGroup), OrderItemType.OrderGroup)]
[JsonSubtypes.KnownSubType(typeof(ItemOrderItem), OrderItemType.OrderItem)]
[JsonSubtypes.KnownSubType(typeof(ItemComponent), OrderItemType.Component)]
[JsonSubtypes.KnownSubType(typeof(ItemPart), OrderItemType.Part)]
[JsonSubtypes.KnownSubType(typeof(ItemResource), OrderItemType.Resource)]
[JsonSubtypes.KnownSubType(typeof(ItemPrice), OrderItemType.Price)]
[DebuggerDisplay("Id={Id}, Number={ArticleNumber}")]
public class ItemBase
{
    #region JsonExtensionData Member

    [JsonExtensionData]
    public Dictionary<string, object>? JsonExtensionData { get; set; }

    #endregion

    #region (10) Item

    /// <summary>
    /// Gets or sets the type of the item entity.
    /// </summary>
    public virtual OrderItemType Type { get;  }

    #endregion

    #region (20) Production

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Gets or sets the state of the entity.
    /// </summary>
    public OrderItemState State { get; set; } = OrderItemState.New;

    /// <summary>
    /// Barcode used to identify a production entity.
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the production entity.
    /// </summary>
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public double? Width { get; set; }

    #endregion

    #region (30) Date/Times

    /// <summary>
    /// Gets or sets the planned end date of the  entity.
    /// </summary>
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the started at date of the  entity.
    /// </summary>
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>Gets or sets the completed date planned</summary>
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the completed at date of the  entity.
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned delivery date of the  entity.
    /// </summary>
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    #endregion

    #region (80) Additional data

    /// <summary>
    /// Gets or sets the notes of the  entity.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    #endregion

    /// <summary>
    /// Custom properties
    /// </summary>
    public IDictionary<string, string>? CustomProperties { get; set; }



    /// <inheritdoc />
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
}