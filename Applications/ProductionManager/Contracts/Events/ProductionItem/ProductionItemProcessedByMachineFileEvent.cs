using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Events.ProductionItem;

#pragma warning disable CS8618

// Note: This is preliminary code and is subject to change

/// <summary>
/// Represents an event that occurs when a machine has processed a file-based input.
/// </summary>
[AppEvent("ProductionItemProcessedByMachineFileEvent")]
public class ProductionItemProcessedByMachineFileEvent : AppEvent
{
    /// <summary>
    /// The machine identifier.
    /// </summary>
    [Required]
    [JsonProperty(Order = 10)]
    public string MachineNumber { get; set; }

    /// <summary>
    /// The file content.
    /// </summary>
    [Required]
    [JsonProperty(Order = 20)]
    public string FileContent { get; set; }

    /// <summary>
    /// The file name.
    /// </summary>
    [JsonProperty(Order = 15)]
    public string? FileName { get; set; }

    /// <summary>
    /// The mapping between column names and their corresponding values.
    /// </summary>
    [JsonProperty(Order = 30)]
    public Dictionary<string, Dictionary<int, string>>? ColumnMapping { get; set; }
}