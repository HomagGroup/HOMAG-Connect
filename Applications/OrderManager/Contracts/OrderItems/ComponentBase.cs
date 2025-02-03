using System.ComponentModel;

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// A (hardware) component.
/// </summary>
public abstract class ComponentBase : Base
{
    /// <summary>
    /// Gets or sets the unit system.
    /// </summary>
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    /// <summary>
    /// Barcode used to identify a production entity.
    /// </summary>
    [JsonProperty(Order = 1)]
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the production entity.
    /// </summary>
    [DefaultValue(1)]
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 10)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 20)]
    public double? Width { get; set; }

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
    /// Gets or sets the quantity planned.
    /// </summary>
    public int? QuantityPlanned { get; set; }

    /// <summary>
    /// Gets or sets the production route.
    /// </summary>
    public string? ProductionRoute { get; set; }

    /// <summary>
    /// Gets or sets the production order type.
    /// </summary>
    public string? ProductionOrderType { get; set; }

    /// <summary>
    /// Gets or sets the state of the entity.
    /// </summary>
    [JsonProperty(Order = 2)]
    [DefaultValue(State.New)]
    public State State { get; set; } = State.New;
}