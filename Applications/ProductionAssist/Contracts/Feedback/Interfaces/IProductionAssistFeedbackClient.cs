namespace HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;

/// <summary>
/// Interface for the ProductionAssist client.
/// </summary>
public interface IProductionAssistFeedbackClient
{
    /// <summary>
    /// Report a production entity as finished.
    /// </summary>
    /// <see href="https://docs.homag.cloud/docs/en/productionassist-feedback-tutorial-report-parts-as-finished</see>.
    Task ReportAsFinished(Guid workstationId, string identification, int quantity, DateTimeOffset? timestamp, string? source = null);
}