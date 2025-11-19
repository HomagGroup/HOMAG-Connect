namespace HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;

/// <summary>
/// Interface for the ProductionAssist client.
/// </summary>
public interface IProductionAssistFeedbackClient
{ 
    /// <summary>
    /// Report a production entity as finished.
    /// </summary>
    Task ReportAsFinished(Guid workstationId, string identification, int quantity, DateTimeOffset? timestamp, string? source = null);
}