using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Import;

/// <summary>
/// Actions that can be performed on import. Serialized as the member name.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ImportOrderRequestAction
{
    /// <summary>
    /// Creates a new order in productionManager using the provided data.
    /// </summary>
    ImportOnly
}