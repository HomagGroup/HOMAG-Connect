using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Import;

/// <summary>
/// Order import operation result
/// </summary>
public class ImportOrderResult
{
    /// <summary>
    /// Gets or sets the corellation Id for the import job
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid CorrelationId { get; set; }
}