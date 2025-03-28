using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.ProductionAssist.Contracts;
using HomagConnect.ProductionAssist.Contracts.Cutting;
using HomagConnect.ProductionAssist.Contracts.Feedback;
using HomagConnect.ProductionAssist.Contracts.Nesting;

namespace HomagConnect.ProductionAssist.Client
{
    /// <inheritdoc cref="IProductionAssistFeedbackClient" />
    public class ProductionAssistFeedbackClient : ServiceBase, IProductionAssistFeedbackClient
    {
        #region IProductionAssistFeedbackClient Members

        /// <inheritdoc />
        public async Task<IEnumerable<FeedbackWorkstation>> GetWorkstations()
        {
            const string uri = "api/productionAssist/feedback/workstations";

            return await RequestEnumerable<FeedbackWorkstation>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task ReportAsFinished(Guid workstationId, string identification, int quantity, DateTimeOffset? timestamp)
        {
            var uri = $"api/productionAssist/feedback/workstations";

            var feedbackRequest = new FeedbackRequest
            {
                WorkstationId = workstationId,
                Identification = identification,
                Quantity = quantity,
                Timestamp = timestamp
            };

            await PostObject(new Uri(uri, UriKind.Relative), feedbackRequest);
        }

        #region Cutting

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

        #endregion Cutting

        #region Nesting

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

        #endregion Nesting

        #endregion

        #region Constructors

        /// <inheritdoc />
        public ProductionAssistFeedbackClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ProductionAssistFeedbackClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public ProductionAssistFeedbackClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion
    }
}