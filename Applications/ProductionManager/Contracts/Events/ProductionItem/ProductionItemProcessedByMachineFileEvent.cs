using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Events.ProductionItem;

#pragma warning disable CS8618

// Note: This is preliminary code and is subject to change

/// <summary>
/// Gets or sets an event that occurs when a machine has processed a file-based input.
/// </summary>
[AppEvent(nameof(ProductionManager), nameof(ProductionItemProcessedByMachineFileEvent))]
public class ProductionItemProcessedByMachineFileEvent : AppEvent
{
    /// <summary>
    /// Gets or sets the machine identifier.
    /// </summary>
    [Required]
    [JsonProperty(Order = 10)]
    public string MachineNumber { get; set; }

    /// <summary>
    /// Gets or sets the file content.
    /// </summary>
    [Required]
    [JsonProperty(Order = 20)]
    public string FileContent { get; set; }

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    [JsonProperty(Order = 15)]
    public string? FileName { get; set; }

    /// <summary>
    /// Gets or sets the mapping between column names and their corresponding values.
    /// </summary>
    [JsonProperty(Order = 30)]
    public Dictionary<int, string>? ColumnMapping { get; set; }
}