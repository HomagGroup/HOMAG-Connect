using System.ComponentModel;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Orders;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Order data
/// </summary>
[DebuggerDisplay("OrderName={OrderName}")]
public class OrderOverview
{
    /// <summary>
    /// The unique id of this order
    /// </summary>
    [JsonProperty(Order = 100)]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets the status of the order.
    /// </summary>
    [JsonProperty(Order = 101)]
    [DefaultValue(OrderState.New)]
    public OrderState State { get; set; } = OrderState.New;

    /// <summary>
    /// The number of the order
    /// </summary>
    [JsonProperty(Order = 110)]
    public string? OrderNumber { get; set; }

    /// <summary>
    /// The order number from the preceding system
    /// </summary>
    [JsonProperty(Order = 111)]
    public string? OrderNumberExternal { get; set; }

    /// <summary>
    /// The name of the order
    /// </summary>
    [JsonProperty(Order = 112)]
    public string OrderName { get; set; } = null!;

    /// <summary>
    /// The description of the order
    /// </summary>
    [JsonProperty(Order = 113)]
    public string? OrderDescription { get; set; }

    /// <summary>
    /// The project of the order
    /// </summary>
    [JsonProperty(Order = 120)]
    public string? Project { get; set; }

    /// <summary>
    /// The description of the order
    /// </summary>
    [JsonProperty(Order = 121)]
    public string? PersonInCharge { get; set; }

    /// <summary>
    /// The date the order was created at
    /// </summary>
    [JsonProperty(Order = 122)]
    public DateTimeOffset OrderDate { get; set; }

    /// <summary>
    /// Gets the planned delivery date of this order.
    /// </summary>
    [JsonProperty(Order = 123)]
    public DateTimeOffset? DeliveryDatePlanned { get; set; }
}