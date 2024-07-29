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
    /// Production entity id
    /// </summary>
    public string? ProductionEntityId { get; set; }
    
    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }
}