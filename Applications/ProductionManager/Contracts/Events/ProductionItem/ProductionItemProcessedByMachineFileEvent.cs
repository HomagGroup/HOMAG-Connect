using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Events;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Events.ProductionItem;

#pragma warning disable CS8618

// Note: This is preliminary code and is subject to change

/// <summary>
/// Event that occurs when a machine has processed a file-based input.
/// </summary>
/// <example>
/// { "id": "0b9f6c1e-52a8-4a3e-9d52-8d3b0c1f7a10", "timestamp": "2025-03-14T08:42:15+00:00", "subscriptionId": "c2f1a6d4-7e53-4b8a-b0c9-1a2d3e4f5a6b", "key": "ProductionItemProcessedByMachineFileEvent",
/// "machineNumber": "1-001-01-0001", "fileName": "kitchen.mpr", "fileContent": "PART-1001;2", "columnMapping": { "Parts": { "0": "Identification", "1": "Quantity" } } }
/// </example>
[AppEvent("ProductionItemProcessedByMachineFileEvent")]
public class ProductionItemProcessedByMachineFileEvent : AppEvent
{
    /// <summary>
    /// Identifier of the machine that processed the file.
    /// </summary>
    /// <example>1-001-01-0001</example>
    [Required]
    [JsonProperty(Order = 10)]
    public string MachineNumber { get; set; }

    /// <summary>
    /// Content of the processed file.
    /// </summary>
    /// <example>PART-1001;2</example>
    [Required]
    [JsonProperty(Order = 20)]
    public string FileContent { get; set; }

    /// <summary>
    /// Name of the processed file. Optional.
    /// </summary>
    /// <example>kitchen.mpr</example>
    [JsonProperty(Order = 15)]
    public string? FileName { get; set; }

    /// <summary>
    /// Mapping of column positions to column names, grouped by data section. Optional.
    /// </summary>
    /// <example>{ "Parts": { "0": "Identification", "1": "Quantity" } }</example>
    [JsonProperty(Order = 30)]
    public Dictionary<string, Dictionary<int, string>>? ColumnMapping { get; set; }
}