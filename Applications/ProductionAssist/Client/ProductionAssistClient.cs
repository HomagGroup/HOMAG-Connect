using System;
using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.ProductionAssist.Client
{
    /// <summary>
    /// ProductionAssistClient
    /// </summary>
    public class ProductionAssistClient : ServiceBase
    {
        /// <summary>
        /// ProductionAssistCuttingClient
        /// </summary>
        public ProductionAssistCuttingClient Cutting { get; set; }

        /// <summary>
        /// ProductionAssistFeedbackClient
        /// </summary>
        public ProductionAssistFeedbackClient Feedback { get; set; }

        /// <summary>
        /// ProductionAssistNestingClient
        /// </summary>
        public ProductionAssistNestingClient Nesting { get; set; }

        #region Constructors

        /// <inheritdoc />
        public ProductionAssistClient(HttpClient client) : base(client)
        {
            Cutting = new ProductionAssistCuttingClient(client);
            Nesting = new ProductionAssistNestingClient(client);
            Feedback = new ProductionAssistFeedbackClient(client);
        }

        /// <inheritdoc />
        public ProductionAssistClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey)
        {
            Cutting = new ProductionAssistCuttingClient(subscriptionOrPartnerId, authorizationKey);
            Nesting = new ProductionAssistNestingClient(subscriptionOrPartnerId, authorizationKey);
            Feedback = new ProductionAssistFeedbackClient(subscriptionOrPartnerId, authorizationKey);
        }

        /// <inheritdoc />
        public ProductionAssistClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri)
        {
            Cutting = new ProductionAssistCuttingClient(subscriptionOrPartnerId, authorizationKey, baseUri);
            Nesting = new ProductionAssistNestingClient(subscriptionOrPartnerId, authorizationKey, baseUri);
            Feedback = new ProductionAssistFeedbackClient(subscriptionOrPartnerId, authorizationKey, baseUri);
        }

        #endregion Constructors
    }
}