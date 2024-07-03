using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HomagConnect.Base.Services;
using HomagConnect.ProductionAssist.Contracts;
using HomagConnect.ProductionAssist.Contracts.Feedback;
using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Client
{
    /// <inheritdoc cref="IProductionAssistFeedbackClient" />
    public class ProductionAssistFeedbackClient : ServiceBase, IProductionAssistFeedbackClient
    {
        #region IProductionAssistFeedbackClient Members

        /// <inheritdoc />
        public async Task<IEnumerable<FeedbackWorkstation>> GetWorkstationsAsync()
        {
            const string uri = "api/productionAssist/feedback/workplaces";

            return await RequestEnumerable<FeedbackWorkstation>(new Uri(uri, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task ReportAsFinishedAsync(Guid workstationId, string productionEntityId, int quantity)
        {
            var uri = $"api/productionAssist/feedback/workplaces/{workstationId}";

            var feedbackRequest = new FeedbackRequest
            {
                WorkstationId = workstationId,
                ProductionEntityId = productionEntityId,
                Quantity = quantity
            };

            var payload = JsonConvert.SerializeObject(feedbackRequest);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            await PostObject(new Uri(uri, UriKind.Relative), content);
        }

        #endregion

        #region Constructors

        /// <inheritdoc />
        public ProductionAssistFeedbackClient(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public ProductionAssistFeedbackClient(Guid subscriptionId, string authorizationKey) : base(subscriptionId, authorizationKey) { }

        /// <inheritdoc />
        public ProductionAssistFeedbackClient(Guid subscriptionId, string authorizationKey, Uri? baseUri) : base(subscriptionId, authorizationKey, baseUri) { }

        #endregion
    }
}