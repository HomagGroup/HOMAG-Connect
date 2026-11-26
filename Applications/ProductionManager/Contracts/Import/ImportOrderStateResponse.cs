using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Current state of an order import, including the created order once the import succeeded.
/// </summary>
/// <example>
/// { "orderId": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "state": "Succeeded", "link": "https://example.com/orders/7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c" }
/// </example>
public class ImportOrderStateResponse
{
    /// <summary>
    /// Link to the imported order. Only set when the import succeeded.
    /// </summary>
    [JsonProperty(Order = 3)]
    public Uri? Link { get; set; }

    /// <summary>
    /// Identifier of the imported order. Only set when the order was created.
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid? OrderId { get; set; }

    /// <summary>
    /// Current state of the import.
    /// </summary>
    [JsonProperty(Order = 2)]
    public ImportState State { get; set; }

    /// <summary>
    /// Error message of the import. Only set when the state is Error.
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? ErrorDetails { get; set; }
}
