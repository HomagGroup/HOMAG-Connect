using HomagConnect.ProductionAssist.Contracts.Feedback;

namespace HomagConnect.ProductionAssist.Contracts;

/// <summary>
/// Interface for the ProductionAssist client.
/// </summary>
public interface IProductionAssistFeedbackClient
{
    /// <summary>
    /// Retrieve the list of configured feedback workstations.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<FeedbackWorkstation>> GetWorkstationsAsync();

    /// <summary>
    /// Report a production entity as finished.
    /// </summary>
    Task ReportAsFinishedAsync(Guid workstationId, string productionEntityId, int quantity);
}