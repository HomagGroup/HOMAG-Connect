namespace HomagConnect.ProductionAssist.Contracts.Cutting.Interfaces
{
    /// <summary>
    /// Provides operations to manage cutting jobs, including retrieval and deletion of cutting job data.
    /// </summary>
    /// <see href="https://docs.homag.cloud/docs/en/productionassist-cutting-tutorial-first-steps">Learn more about productionAssist cutting</see>.
    public interface IProductionAssistCuttingClient
    {
        #region Read

        /// <summary>
        /// Retrieve a cutting job by id.
        /// </summary>
        /// <param name="cuttingJobId">The unique identifier of the cutting job.</param>
        /// <returns>The cutting job.</returns>
        Task<CuttingJob> GetCuttingJob(Guid cuttingJobId);

        /// <summary>
        /// Retrieve the list of all cutting jobs.
        /// </summary>
        /// <returns>The cutting jobs.</returns>
        Task<IEnumerable<CuttingJob>> GetCuttingJobs();

        /// <summary>
        /// Retrieve cutting jobs by id.
        /// </summary>
        /// <param name="cuttingJobIds">The unique identifiers of the cutting jobs.</param>
        /// <returns>The cutting jobs.</returns>
        Task<IEnumerable<CuttingJob>> GetCuttingJobs(Guid[] cuttingJobIds);

        #endregion Read

        #region Delete

        /// <summary>
        /// Delete a cutting job.
        /// </summary>
        /// <param name="cuttingJobId">The unique identifier of the cutting job to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCuttingJob(Guid cuttingJobId);

        /// <summary>
        /// Delete cutting jobs
        /// </summary>
        /// <param name="cuttingJobIds">The unique identifiers of the cutting jobs to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCuttingJobs(Guid[] cuttingJobIds);

        #endregion Delete
    }
}