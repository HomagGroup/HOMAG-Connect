using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Response returned when an order import was accepted.
/// </summary>
/// <example>
/// { "correlationId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c" }
/// </example>
public class ImportOrderResponse
{
    /// <summary>
    /// Identifier of the import job. Use it to query the import state.
    /// </summary>
    /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
    [JsonProperty(Order = 1)]
    public Guid CorrelationId { get; set; }
}
