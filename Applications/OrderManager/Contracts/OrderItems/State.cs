using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Production and delivery state of an order item. Serialized as the member name.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum State
{
    /// <summary>
    /// State is not known (fallback for new or unrecognized values).    /// </summary>
    Unknown,

    /// <summary>
    /// The item is new and has not been released yet.    /// </summary>
    New,

    /// <summary>
    /// The item is ready for production.    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// The item is in production.    /// </summary>
    InProduction,

    /// <summary>
    /// Production of the item is completed.    /// </summary>
    Completed,

    /// <summary>
    /// The item has been distributed.    /// </summary>
    Distributed,

    /// <summary>
    /// The item has been delivered.    /// </summary>
    Delivered,

    /// <summary>
    /// The item has been archived.    /// </summary>
    Archived
}