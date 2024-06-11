using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Order import response
/// </summary>
public class ImportOrderResponse
{
    /// <summary>
    /// Gets or sets the link to the optimization
    /// </summary>
    [JsonProperty(Order = 3)]
    public Uri? Link { get; set; }

    /// <summary>
    /// Gets or sets the order id
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid OrderId { get; set; }
}