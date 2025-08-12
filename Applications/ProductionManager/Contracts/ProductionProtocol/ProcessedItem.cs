using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionProtocol;

/// <summary>
/// Represents a processed item in a production protocol.
/// </summary>
public class ProcessedItem
{
    /// <summary>
    /// Gets or sets the additional properties associated with the processed item.
    /// </summary>
    [JsonProperty(Order = 90)]
    [JsonExtensionData]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Gets or sets the machine name where the item was processed.
    /// </summary>
    public string? MachineName { get; set; }

    /// <summary>
    /// Gets or sets the machine number where the item was processed.
    /// </summary>
    public string? MachineNumber { get; set; }

    /// <summary>
    /// Gets or sets the machine type where the item was processed.
    /// </summary>
    public virtual MachineType MachineType { get; set; } = MachineType.Unknown;

    /// <summary>
    /// Gets or sets the source of the processing information.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Gets or sets the subscription identifier associated with the processed item.
    /// </summary>
    public Guid SubscriptionId { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the item was processed.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }
}