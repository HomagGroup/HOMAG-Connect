using System.ComponentModel;

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Base class of order items that represent articles such as parts, boards or hardware components.
/// </summary>
public abstract class ComponentBase : Base
{
    #region Article
    
    /// <summary>
    /// Article number. Optional.
    /// </summary>
    /// <example>HW-4711</example>
    [JsonProperty(Order = 11)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Description of the article. Optional.
    /// </summary>
    /// <example>Hinge 110 degrees</example>
    [JsonProperty(Order = 12)]
    public string? Description { get; set; }
    
    /// <summary>
    /// Length. Optional.
    /// </summary>
    /// <example>720</example>
    [JsonProperty(Order = 13)]
    public double? Length { get; set; }

    /// <summary>
    /// Width. Optional.
    /// </summary>
    /// <example>596</example>
    [JsonProperty(Order = 14)]
    public double? Width { get; set; }

    /// <summary>
    /// Article group. Optional.
    /// </summary>
    /// <example>Hinges</example>
    [JsonProperty(Order = 17)]
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Procurement type. Optional. Values:
    /// <list type="bullet">
    /// <item><description><c>PTO</c> - purchase to order</description></item>
    /// <item><description><c>MTO</c> - make to order</description></item>
    /// <item><description><c>MTS</c> - make to stock</description></item>
    /// </list>
    /// </summary>
    /// <example>MTO</example>
    [JsonProperty(Order = 18)]
    public string? ProcurementType { get; set; }

    #endregion

    #region Production

    /// <summary>
    /// Production state. Defaults to <c>New</c>.
    /// </summary>
    /// <example>New</example>
    [JsonProperty(Order = 20)]
    [DefaultValue(State.New)]
    public State State { get; set; } = State.New;

    /// <summary>
    /// Barcode used to identify the item in production. Optional.
    /// </summary>
    /// <example>4006381333931</example>
    [JsonProperty(Order = 21)]
    public string? Barcode { get; set; }
    
    /// <summary>
    /// Quantity of the item. Defaults to 1.
    /// </summary>
    /// <example>2</example>
    [JsonProperty(Order = 22)]
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Identifier of the item in an external system, which can be used as a reference. Optional.
    /// </summary>
    /// <example>ext-7781</example>
    [JsonProperty(Order = 23)]
    public string? ExternalSystemId { get; set; }

    #endregion

    /// <summary>
    /// Unit system of the dimensions. Defaults to <c>Metric</c>.
    /// </summary>
    /// <example>Metric</example>
    [JsonProperty(Order = 999)]
    [DefaultValue(UnitSystem.Metric)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

}