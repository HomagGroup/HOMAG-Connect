namespace HomagConnect.ProductionAssist.Contracts.Nesting.Interfaces
{
    /// <summary>
    /// Provides operations to manage nesting jobs, including retrieval and deletion of nesting job data.
    /// <see href="https://docs.homag.cloud/docs/en/productionassist-nesting-tutorial-first-steps">Learn more about productionAssist nesting</see>.
    /// </summary>
    public interface IProductionAssistNestingClient
    {
        #region Read

        /// <summary>
        /// Retrieve a nesting job by id.
        /// </summary>
        /// <param name="nestingJobId">The unique identifier of the nesting job.</param>
        /// <returns>The nesting job.</returns>
        Task<NestingJob> GetNestingJob(Guid nestingJobId);

        /// <summary>
        /// Retrieve the list of all nesting jobs.
        /// </summary>
        /// <returns>The nesting jobs.</returns>
        Task<IEnumerable<NestingJob>> GetNestingJobs();

        /// <summary>
        /// Retrieve nesting jobs by id.
        /// </summary>
        /// <param name="nestingJobIds">The unique identifiers of the nesting jobs.</param>
        /// <returns>The nesting jobs.</returns>
        Task<IEnumerable<NestingJob>> GetNestingJobs(Guid[] nestingJobIds);

        #endregion Read

        #region Delete

        /// <summary>
        /// Delete a nesting job.
        /// </summary>
        /// <param name="nestingJobId">The unique identifier of the nesting job to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteNestingJob(Guid nestingJobId);

        /// <summary>
        /// Delete nesting jobs
        /// </summary>
        /// <param name="nestingJobIds">The unique identifiers of the nesting jobs to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteNestingJobs(Guid[] nestingJobIds);

        #endregion Delete
    }
}