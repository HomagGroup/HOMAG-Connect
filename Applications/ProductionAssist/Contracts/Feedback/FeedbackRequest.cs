namespace HomagConnect.ProductionAssist.Contracts.Feedback;

/// <summary>
/// Feedback request.
/// </summary>
public class FeedbackRequest 
{
    /// <summary>
    /// Workstation id
    /// </summary>
    public Guid WorkstationId { get; set; }

    /// <summary>
    /// Identification
    /// </summary>
    public string Identification { get; set; }
    
    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or Sets the timestamp
    /// </summary>
    public DateTimeOffset? Timestamp { get; set; }
}