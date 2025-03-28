using HomagConnect.ProductionAssist.Contracts.Cutting;
using HomagConnect.ProductionAssist.Contracts.Feedback;
using HomagConnect.ProductionAssist.Contracts.Nesting;

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
    Task<IEnumerable<FeedbackWorkstation>> GetWorkstations();

    /// <summary>
    /// Report a production entity as finished.
    /// </summary>
    Task ReportAsFinished(Guid workstationId, string identification, int quantity, DateTimeOffset? timestamp);

    #region Cutting

    /// <summary>
    /// Retrieve a cutting job by id.
    /// </summary>
    Task<CuttingJob> GetCuttingJob(Guid cuttingJobId);

    /// <summary>
    /// Retrieve the list of all cutting jobs.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CuttingJob>> GetCuttingJobs();

    /// <summary>
    /// Retrieve cutting jobs by id.
    /// </summary>
    Task<IEnumerable<CuttingJob>> GetCuttingJobs(Guid[] cuttingJobIds);

    /// <summary>
    /// Delete a cutting job.
    /// </summary>
    Task DeleteCuttingJob(Guid cuttingJobId);

    /// <summary>
    /// Delete cutting jobs
    /// </summary>
    Task DeleteCuttingJobs(Guid[] cuttingJobIds);

    #endregion Cutting

    #region Nesting

    /// <summary>
    /// Retrieve a nesting job by id.
    /// </summary>
    Task<NestingJob> GetNestingJob(Guid nestingJobId);

    /// <summary>
    /// Retrieve the list of all nesting jobs.
    /// </summary>
    Task<IEnumerable<NestingJob>> GetNestingJobs();

    /// <summary>
    /// Retrieve nesting jobs by id.
    /// </summary>
    Task<IEnumerable<NestingJob>> GetNestingJobs(Guid[] nestingJobIds);

    /// <summary>
    /// Delete a nesting job.
    /// </summary>
    Task DeleteNestingJob(Guid nestingJobId);

    /// <summary>
    /// Delete nesting jobs
    /// </summary>
    Task DeleteNestingJobs(Guid[] nestingJobIds);

    #endregion Nesting
}