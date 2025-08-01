using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.ProductionAssist.Contracts.Feedback;
using HomagConnect.ProductionAssist.Contracts.Feedback.Interfaces;

namespace HomagConnect.ProductionAssist.Client
{
    /// <inheritdoc cref="IProductionAssistFeedbackClient" />
    public class ProductionAssistFeedbackClient : ServiceBase, IProductionAssistFeedbackClient
    {
        #region IProductionAssistFeedbackClient Members

        /// <inheritdoc />
        public async Task<IEnumerable<FeedbackWorkstation>?> GetWorkstations()
        {
            const string uri = "api/productionAssist/feedback/workstations";

            return await RequestEnumerable<FeedbackWorkstation>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task ReportAsFinished(Guid workstationId, string identification, int quantity, DateTimeOffset? timestamp, string source)
        {
            var uri = $"api/productionAssist/feedback/workstations";

            var feedbackRequest = new FeedbackRequest
            {
                WorkstationId = workstationId,
                Identification = identification,
                Quantity = quantity,
                Timestamp = timestamp,
                Source = source
            };

            await PostObject(new Uri(uri, UriKind.Relative), feedbackRequest);
        }

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