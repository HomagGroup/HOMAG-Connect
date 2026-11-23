using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Actions that can be performed when an order is imported. Serialized as the member name.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum ImportOrderRequestAction
{
    /// <summary>
    /// Creates a new order in productionManager using the provided data.
    /// </summary>
    ImportOnly
}
