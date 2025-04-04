using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.ProductionAssist.Contracts.Cutting;
using HomagConnect.ProductionAssist.Contracts.Cutting.Interfaces;

namespace HomagConnect.ProductionAssist.Client
{
    /// <inheritdoc cref="IProductionAssistCuttingClient" />
    public class ProductionAssistCuttingClient : ServiceBase, IProductionAssistCuttingClient
    {
        #region Delete

        /// <inheritdoc />
        public async Task DeleteCuttingJob(Guid cuttingJobId)
        {
            await DeleteCuttingJobs([cuttingJobId]);
        }

        /// <inheritdoc />
        public async Task DeleteCuttingJobs(Guid[] cuttingJobIds)
        {
            var uri = $"/api/productionAssist/cutting/jobs?cuttingJobId={cuttingJobIds[0]}";
            for (var i = 1; i < cuttingJobIds.Length; i++)
            {
                uri += $"&cuttingJobId={cuttingJobIds[i]}";
            }

            await DeleteObject(new Uri(uri, UriKind.Relative));
        }

        #endregion Delete

        #region Read

        /// <inheritdoc />
        public async Task<IEnumerable<CuttingJob>> GetCuttingJobs()
        {
            var uri = $"/api/productionAssist/cutting/jobs";
            return await RequestEnumerable<CuttingJob>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CuttingJob>> GetCuttingJobs(Guid[] cuttingJobIds)
        {
            var uri = $"/api/productionAssist/cutting/jobs?cuttingJobId={cuttingJobIds[0]}";
            for (var i = 1; i < cuttingJobIds.Length; i++)
            {
                uri += $"&cuttingJobId={cuttingJobIds[i]}";
            }

            return await RequestEnumerable<CuttingJob>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<CuttingJob> GetCuttingJob(Guid cuttingJobId)
        {
            return (await GetCuttingJobs([cuttingJobId])).FirstOrDefault();
        }

        #endregion Read

        #region Constructors

        /// <inheritdoc />
        public ProductionAssistCuttingClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ProductionAssistCuttingClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public ProductionAssistCuttingClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion
    }
}