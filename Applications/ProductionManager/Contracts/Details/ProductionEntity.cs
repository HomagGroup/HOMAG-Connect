using JsonSubTypes;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Details;

/// <summary>
/// Production entity
/// </summary>
[JsonConverter(typeof(JsonSubtypes), nameof(Type))]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityOrderItem), ProductionEntityType.OrderItem)]
[JsonSubtypes.KnownSubType(typeof(ProductionEntityProductionOrder), ProductionEntityType.ProductionOrder)]
public class ProductionEntity
{
    /// <summary>
    /// Gets or sets the notes of the production entity.
    /// </summary>
    [JsonProperty(Order = 99)]
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the production entity.
    /// </summary>
    [JsonProperty(Order = 2)]
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the type of the production entity.
    /// </summary>
    [JsonProperty(Order = 1)]
    public virtual ProductionEntityType Type { get; set; }
}