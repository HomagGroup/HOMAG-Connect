namespace HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;

/// <summary>
/// Interface for the ProductionAssist client.
/// </summary>
public interface IProductionAssistFeedbackClient
{
    /// <summary>
    /// Report a production entity as finished.
    /// </summary>
    /// <param name="workstationId">The unique identifier of the workstation the feedback is reported for.</param>
    /// <param name="identification">The identifier of the production item the feedback is reported for.</param>
    /// <param name="quantity">The number of items the feedback applies to.</param>
    /// <param name="timestamp">The date and time the feedback applies to. Optional.</param>
    /// <param name="source">The person or program that triggered the feedback. Optional.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <see href="https://docs.homag.cloud/docs/en/productionassist-feedback-tutorial-report-parts-as-finished">Learn more about reporting parts as finished</see>.
    Task ReportAsFinished(Guid workstationId, string identification, int quantity, DateTimeOffset? timestamp, string? source = null);
}