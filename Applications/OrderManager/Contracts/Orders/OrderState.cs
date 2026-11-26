using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.Orders;

/// <summary>
/// State of an order. Serialized as the member name.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum OrderState
{
    /// <summary>
    /// The order was imported successfully and has not been released yet.
    /// </summary>
    New,

    /// <summary>
    /// The order has been released and is ready for production.
    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// Production of the order is in progress.
    /// </summary>
    InProgress,

    /// <summary>
    /// All production orders of the order have been completed.
    /// </summary>
    Completed,

    /// <summary>
    /// The order has been archived.
    /// </summary>
    Archived,

    /// <summary>
    /// An order group has been created or updated and the order data is still being generated.
    /// </summary>
    NewInCalculation,

    /// <summary>
    /// The order is a draft that is not yet finalized.
    /// </summary>
    Draft,
}