using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.ProductionAssist.Contracts.Nesting;
using HomagConnect.ProductionAssist.Contracts.Nesting.Interfaces;

namespace HomagConnect.ProductionAssist.Client
{
    /// <inheritdoc cref="IProductionAssistNestingClient" />
    public class ProductionAssistNestingClient : ServiceBase, IProductionAssistNestingClient
    {
        #region Delete

        /// <inheritdoc />
        public async Task DeleteNestingJob(Guid nestingJobId)
        {
            await DeleteNestingJobs([nestingJobId]);
        }

        /// <inheritdoc />
        public async Task DeleteNestingJobs(Guid[] nestingJobIds)
        {
            var uri = $"/api/productionAssist/nesting/jobs?nestingJobId={nestingJobIds[0]}";
            for (var i = 1; i < nestingJobIds.Length; i++)
            {
                uri += $"&nestingJobId={nestingJobIds[i]}";
            }

            await DeleteObject(new Uri(uri, UriKind.Relative));
        }

        #endregion Delete

        #region Read

        /// <inheritdoc />
        public async Task<IEnumerable<NestingJob>> GetNestingJobs()
        {
            var uri = $"/api/productionAssist/nesting/jobs";
            return await RequestEnumerable<NestingJob>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<NestingJob>> GetNestingJobs(Guid[] nestingJobIds)
        {
            var uri = $"/api/productionAssist/nesting/jobs?nestingJobId={nestingJobIds[0]}";
            for (var i = 1; i < nestingJobIds.Length; i++)
            {
                uri += $"&nestingJobId={nestingJobIds[i]}";
            }

            return await RequestEnumerable<NestingJob>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<NestingJob> GetNestingJob(Guid nestingJobId)
        {
            return (await GetNestingJobs([nestingJobId])).FirstOrDefault();
        }

        #endregion Read

        #region Constructors

        /// <inheritdoc />
        public ProductionAssistNestingClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ProductionAssistNestingClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public ProductionAssistNestingClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion
    }
}