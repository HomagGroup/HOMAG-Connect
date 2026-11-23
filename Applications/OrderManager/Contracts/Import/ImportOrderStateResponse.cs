using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.Import;

/// <summary>
/// Current state of an order import, including the imported order once available.
/// </summary>
/// <example>
/// { "orderId": "3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "state": "Succeeded", "link": "https://example.com/orders/3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b", "errorDetails": null }
/// </example>
public class ImportOrderStateResponse
{
    /// <summary>
    /// Link to the imported order. Optional; available once the import has succeeded.
    /// </summary>
    /// <example>https://example.com/orders/3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b</example>
    [JsonProperty(Order = 3)]
    public Uri? Link { get; set; }

    /// <summary>
    /// Unique identifier of the imported order. Optional; available once the order was created.
    /// </summary>
    /// <example>3f2b8c1e-5a47-4d6e-9b0a-7c1d2e3f4a5b</example>
    [JsonProperty(Order = 1)]
    public Guid? OrderId { get; set; }

    /// <summary>
    /// State of the import.
    /// </summary>
    /// <example>Succeeded</example>
    [JsonProperty(Order = 2)]
    public ImportState State { get; set; }

    /// <summary>
    /// Description of the error if the import failed. Optional.
    /// </summary>
    /// <example>Material 'P2_Gold_Craft_Oak_19.0' was not found.</example>
    [JsonProperty(Order = 4)]
    public string? ErrorDetails { get; set; }
}