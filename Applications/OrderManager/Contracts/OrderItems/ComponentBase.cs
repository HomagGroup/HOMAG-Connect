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
    #region Article
    
    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 11)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 12)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 13)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 14)]
    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    [JsonProperty(Order = 17)]
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Gets or sets the procurement type.
    /// </summary>
    [JsonProperty(Order = 18)]
    public string? ProcurementType { get; set; }

    #endregion

    #region Production

    /// <summary>
    /// Gets or sets the state of the entity.
    /// </summary>
    [JsonProperty(Order = 20)]
    [DefaultValue(State.New)]
    public State State { get; set; } = State.New;

    /// <summary>
    /// Barcode used to identify a production entity.
    /// </summary>
    [JsonProperty(Order = 21)]
    public string? Barcode { get; set; }
    
    /// <summary>
    /// Gets or sets the quantity of the production entity.
    /// </summary>
    [JsonProperty(Order = 22)]
    public int Quantity { get; set; } = 1;
    
    #endregion
    
    /// <summary>
    /// Gets or sets the unit system.
    /// </summary>
    [JsonProperty(Order = 999)]
    [DefaultValue(UnitSystem.Metric)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

}