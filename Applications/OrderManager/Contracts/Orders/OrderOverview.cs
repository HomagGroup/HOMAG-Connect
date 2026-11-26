using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Orders;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Summary of an order with its header data, without addresses, additional data and order items.
/// </summary>
/// <example>
/// { "id": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "state": "ReadyForProduction", "orderNumber": "ORD-2025-0042", "orderName": "Kitchen Miller", "orderDate": "2025-04-01T08:30:00+00:00", "hasErrors": false }
/// </example>
[DebuggerDisplay("OrderName={OrderName}")]
public class OrderOverview
{
    /// <summary>
    /// Unique identifier of the order.
    /// </summary>
    /// <example>3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b</example>
    [Key]
    [JsonProperty(Order = 100)]
    public Guid Id { get; set; }

    /// <summary>
    /// Current state of the order. Defaults to <c>New</c>.
    /// </summary>
    /// <example>ReadyForProduction</example>
    [JsonProperty(Order = 101)]
    [DefaultValue(OrderState.New)]
    public OrderState State { get; set; } = OrderState.New;

    /// <summary>
    /// Number of the order. Optional.
    /// </summary>
    /// <example>ORD-2025-0042</example>
    [JsonProperty(Order = 110)]
    [StringLength(19)]
    public string? OrderNumber { get; set; }

    /// <summary>
    /// Order number from the preceding system, for example an ERP system. Optional.
    /// </summary>
    /// <example>ERP-90017</example>
    [JsonProperty(Order = 111)]
    [StringLength(19)]
    public string? OrderNumberExternal { get; set; }

    /// <summary>
    /// Name of the order.
    /// </summary>
    /// <example>Kitchen Miller</example>
    [JsonProperty(Order = 112)]
    [StringLength(100)]
    public string OrderName { get; set; } = null!;

    /// <summary>
    /// Description of the order. Optional.
    /// </summary>
    /// <example>Kitchen with oak fronts</example>
    [JsonProperty(Order = 113)]
    [StringLength(300)]
    public string? OrderDescription { get; set; }

    /// <summary>
    /// Project the order belongs to. Optional.
    /// </summary>
    /// <example>Miller House</example>
    [JsonProperty(Order = 120)]
    [StringLength(100)]
    public string? Project { get; set; }

    /// <summary>
    /// Person in charge of the order. Optional.
    /// </summary>
    /// <example>J. Smith</example>
    [JsonProperty(Order = 121)]
    [StringLength(100)]
    public string? PersonInCharge { get; set; }

    /// <summary>
    /// Date and time of the order.
    /// </summary>
    /// <example>2025-04-01T08:30:00+00:00</example>
    [JsonProperty(Order = 122)]
    [DataType(DataType.Date)]
    public DateTimeOffset OrderDate { get; set; }

    /// <summary>
    /// Planned delivery date and time of the order. Optional.
    /// </summary>
    /// <example>2025-05-15T00:00:00+00:00</example>
    [JsonProperty(Order = 123)]
    [DataType(DataType.Date)]
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    /// <summary>
    /// Planned completion date and time of the order. Optional.
    /// </summary>
    /// <example>2025-05-10T00:00:00+00:00</example>
    [JsonProperty(Order = 125)]
    [DataType(DataType.Date)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Identifier of the order in an external system, which can be used as a reference. Optional.
    /// </summary>
    /// <example>ext-7781</example>
    [JsonProperty(Order = 126)]
    [StringLength(300)]
    public string? ExternalSystemId { get; set; }

    /// <summary>
    /// System the order originates from. Optional.
    /// </summary>
    /// <example>ERP</example>
    [JsonProperty(Order = 128)]
    [StringLength(100)]
    public string? Source { get; set; }

    /// <summary>
    /// <c>true</c> if the order has errors; otherwise <c>false</c>. What counts as an error is defined by orderManager; clients can use the flag to display an error state.
    /// </summary>
    /// <example>false</example>
    [JsonProperty(Order = 127)]
    public bool HasErrors { get; set; }
}