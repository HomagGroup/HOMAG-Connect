using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.ProductionAssist.Contracts;
using HomagConnect.ProductionManager.Contracts.ProductionEntity;

namespace HomagConnect.ProductionAssist.Client
{
    /// <summary>
    /// ProductionAssistClient
    /// </summary>
    public class ProductionAssistClient : ServiceBase, IProductionAssistClient
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

        /// <inheritdoc />
        public async Task<ProductionEntity?> GetOrderItem(string identifier)
        {
            var orderItems = await GetOrderItems([identifier]);

            return orderItems?.FirstOrDefault();
        }

        /// <inheritdoc />
        public async Task<ProductionEntity[]?> GetOrderItems(string[] identifiers)
        {
            const string parameter = "identifier";
            const string endpoint = "api/productionAssist/orderItems";

        var uris = identifiers.Select(i => i.Trim())
                .Where( i =>!string.IsNullOrWhiteSpace(i))
                .Select(code => $"&{parameter}={Uri.EscapeDataString(code)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.TrimStart('&'))
                .Select(p => $"{endpoint}?{p}")
                .Select(c => new Uri(c, UriKind.Relative));

            return (await RequestEnumerableAsync<ProductionEntity>(uris)).ToArray();
        }
    }
}