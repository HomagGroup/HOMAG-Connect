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

    /// <summary>
    /// Public id
    /// </summary>
    public string? PublicId { get; set; }

    /// <summary>
    /// Barcode
    /// </summary>
    public string? Barcode { get; set; }
}