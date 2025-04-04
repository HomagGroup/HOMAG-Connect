namespace HomagConnect.ProductionAssist.Contracts.Cutting.Interfaces
{
    /// <summary>
    /// productionAssist Client for cutting jobs.
    /// </summary>
    public interface IProductionAssistCuttingClient
    {
        #region Read

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

        #endregion Read

        #region Delete

        /// <summary>
        /// Delete a cutting job.
        /// </summary>
        Task DeleteCuttingJob(Guid cuttingJobId);

        /// <summary>
        /// Delete cutting jobs
        /// </summary>
        Task DeleteCuttingJobs(Guid[] cuttingJobIds);

        #endregion Delete
    }
}