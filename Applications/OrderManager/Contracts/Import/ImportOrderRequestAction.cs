using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Import;

/// <summary>
/// Defines the actions which should get performed on import.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ImportOrderRequestAction
{
    /// <summary>
    /// Creates a new order in productionManager using the provided data.
    /// </summary>
    ImportOnly
}