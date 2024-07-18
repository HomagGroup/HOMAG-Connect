using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HomagConnect.Base.Services;
using HomagConnect.ProductionAssist.Contracts;
using HomagConnect.ProductionAssist.Contracts.Feedback;

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
        public async Task ReportAsFinished(Guid workstationId, string productionEntityId, int quantity)
        {
            var uri = $"api/productionAssist/feedback/workstations";

            var feedbackRequest = new FeedbackRequest
            {
                WorkstationId = workstationId,
                ProductionEntityId = productionEntityId,
                Quantity = quantity
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