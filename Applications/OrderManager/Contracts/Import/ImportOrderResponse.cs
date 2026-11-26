using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Import;

/// <summary>
/// Response returned when an order import was accepted.
/// </summary>
/// <example>
/// { "correlationId": "9b2f3a61-4c7d-4e8a-a1b5-6d7e8f901234" }
/// </example>
public class ImportOrderResponse
{
    /// <summary>
    /// Identifier of the import job. Use it to query the import state.
    /// </summary>
    /// <example>9b2f3a61-4c7d-4e8a-a1b5-6d7e8f901234</example>
    [JsonProperty(Order = 1)]
    public Guid CorrelationId { get; set; }
}