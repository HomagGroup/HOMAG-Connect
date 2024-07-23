using System.Runtime.Serialization;

using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityOrderItem), ProductionEntityType.OrderItem)]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityProductionOrder), ProductionEntityType.ProductionOrder)]
public class ProductionEntity : IExtensibleDataObject
{
    #region (10) Article

    /// <summary>
    /// Gets or sets the type of the production entity.
    /// </summary>
    [JsonProperty(Order = 0)]
    public virtual ProductionEntityType Type { get; set; }

    #endregion

    #region (20) Production

    /// <summary>
    /// Gets or sets the id
    /// </summary>
    [JsonProperty(Order = 21)]
    public string? Id { get; set; }

    /// <summary>
    /// Barcode used to identify a production entity.
    /// </summary>
    [JsonProperty(Order = 21)]
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the status of the production entity.
    /// </summary>
    [JsonProperty(Order = 22)]
    public ProductionEntityStatus Status { get; set; } = ProductionEntityStatus.New;

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

    #endregion

    #region (90) IExtensibleDataObject Members

    /// <intheritdoc />
    [JsonProperty(Order = 90)]
    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion
}