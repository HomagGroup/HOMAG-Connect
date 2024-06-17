using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Order import response
/// </summary>
public class ImportOrderStateResponse
{
    /// <summary>
    /// Gets or sets the link to order
    /// </summary>
    [JsonProperty(Order = 3)]
    public Uri? Link { get; set; }

    /// <summary>
    /// Gets or sets the order id
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid OrderId { get; set; }

    /// <summary>
    /// Gets or sets the Import state of the order
    /// </summary>
    [JsonProperty(Order = 2)]
    public ImportState State { get; set; }

    /// <summary>
    /// Gets or sets the Error Text of the import process
    /// </summary>
    [JsonProperty(Order = 4)]
    public string? ErrorDetails { get; set; }
}