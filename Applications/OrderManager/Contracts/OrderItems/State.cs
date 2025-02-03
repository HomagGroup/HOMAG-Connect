using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Item state.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum State
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// New
    /// </summary>
    New,

    /// <summary>
    /// Ready for production
    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// In Production
    /// </summary>
    InProduction,

    /// <summary>
    /// Completed
    /// </summary>
    Completed,
}