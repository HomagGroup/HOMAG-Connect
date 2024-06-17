using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Order import operation response
/// </summary>
public class ImportOrderResponse
{
    /// <summary>
    /// Gets or sets the correlation id for the import job
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid CorrelationId { get; set; }
}