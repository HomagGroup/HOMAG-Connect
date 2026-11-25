using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Feedback;

/// <summary>
/// Request to report production feedback for an item at a workstation.
/// </summary>
/// <example>
/// { "workstationId": "6f9619ff-8b86-d011-b42d-00cf4fc964ff", "identification": "PART-1001", "quantity": 2, "timestamp": "2025-03-14T08:42:15+00:00", "source": "operator@example.com" }
/// </example>
public class FeedbackRequest 
{
    /// <summary>
    /// Unique identifier of the workstation the feedback is reported for.
    /// </summary>
    [JsonProperty(Order = 1)]
    public Guid WorkstationId { get; set; }

    /// <summary>
    /// Identifier of the production item the feedback is reported for.
    /// </summary>
    /// <example>PART-1001</example>
    [JsonProperty(Order = 2)]
    public string Identification { get; set; }

    /// <summary>
    /// Number of items the feedback applies to.
    /// </summary>
    /// <example>2</example>
    [Range(0, int.MaxValue)]
    [JsonProperty(Order = 3)]
    public int Quantity { get; set; }

    /// <summary>
    /// Date and time the feedback applies to. Optional.
    /// </summary>
    /// <example>2025-03-14T08:42:15+00:00</example>
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:g}")]
    [JsonProperty(Order = 4)]
    public DateTimeOffset? Timestamp { get; set; }

    /// <summary>
    /// Person or program that triggered the feedback. Optional.
    /// </summary>
    /// <example>operator@example.com</example>
    [JsonProperty(Order = 5)]
    [StringLength(100)]
    public string? Source { get; set; }
}