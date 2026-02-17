using System;
using System.Net.Http;

using HomagConnect.Base.Client;


// ReSharper disable once CheckNamespace
namespace HomagConnect.Base.Services
{
    /// <summary>
    /// Obsolete base class for service clients. Use <see cref="ClientBase"/> instead.
    /// </summary>
    /// <remarks>
    /// This type is retained for backward compatibility and is marked as obsolete with error.
    /// Migrate to <see cref="ClientBase"/> to avoid compilation errors.
    /// </remarks>
    [Obsolete($"Use {nameof(ClientBase)} instead.")]
    public class ServiceBase: ClientBase
    {
        /// <summary>
        /// Initializes a new instance of the service using an existing <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="client">The HTTP client to use.</param>
        protected ServiceBase(HttpClient client) : base(client) { }

        /// <summary>
        /// Initializes a new instance of the service using subscription/partner id and authorization key.
        /// </summary>
        /// <param name="subscriptionOrPartnerId">Subscription or partner identifier.</param>
        /// <param name="authorizationKey">API authorization key.</param>
        protected ServiceBase(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <summary>
        /// Initializes a new instance of the service using subscription/partner id, authorization key, and base URI.
        /// </summary>
        /// <param name="subscriptionOrPartnerId">Subscription or partner identifier.</param>
        /// <param name="authorizationKey">API authorization key.</param>
        /// <param name="baseUri">Base URI of the API.</param>
        protected ServiceBase(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }
    }
}