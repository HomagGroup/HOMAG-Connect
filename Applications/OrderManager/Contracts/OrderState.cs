using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts;

/// <summary>
/// Order Status Enumeration
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum OrderState
{
    /// <summary>
    /// The order status is unknown. Only fallback for unknown newer states.
    /// </summary>
    Unknown,

    /// <summary>
    /// After a successful import of a customer order, it receives the state "New".
    /// </summary>
    New,

    /// <summary>
    /// After an order has been release it receives the state "ReadyForProduction".
    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// As soon as a production order of a order is in production.
    /// </summary>
    InProduction,

    /// <summary>
    /// As soon as all production orders have been completed, the order receives the state "Completed".
    /// </summary>
    Completed,

    /// <summary>
    /// The order has been archived.
    /// </summary>
    Archived,
}