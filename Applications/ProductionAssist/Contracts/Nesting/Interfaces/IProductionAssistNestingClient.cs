namespace HomagConnect.ProductionAssist.Contracts.Nesting.Interfaces
{
    /// <summary>
    /// productionAssist Client for cutting jobs.
    /// </summary>
    public interface IProductionAssistNestingClient
    {
        #region Read

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

        #endregion Read

        #region Delete

        /// <summary>
        /// Delete a nesting job.
        /// </summary>
        Task DeleteNestingJob(Guid nestingJobId);

        /// <summary>
        /// Delete nesting jobs
        /// </summary>
        Task DeleteNestingJobs(Guid[] nestingJobIds);

        #endregion Delete
    }
}