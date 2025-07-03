using System;
using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client
{
    /// <summary>
    /// Provides access to the MaterialManager API.
    /// </summary>
    public class MaterialManagerClient : ServiceBase
    {
        /// <inheritdoc />
        public MaterialManagerClient(HttpClient client) : base(client)
        {
            Material = new MaterialManagerClientMaterial(client);
            Processing = new MaterialManagerClientProcessing(client);
        }

        /// <inheritdoc />
        public MaterialManagerClient(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey)
        {
            Material = new MaterialManagerClientMaterial(subscriptionOrPartnerId, authorizationKey);
            Processing = new MaterialManagerClientProcessing(subscriptionOrPartnerId, authorizationKey);
        }

        /// <inheritdoc />
        public MaterialManagerClient(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri)
        {
            Material = new MaterialManagerClientMaterial(subscriptionOrPartnerId, authorizationKey, baseUri);
            Processing = new MaterialManagerClientProcessing(subscriptionOrPartnerId, authorizationKey, baseUri);
        }

        /// <summary>
        /// Client for managing materials.
        /// </summary>
        public MaterialManagerClientMaterial Material { get; set; }

        /// <summary>
        /// Client for managing processing.
        /// </summary>
        public MaterialManagerClientProcessing Processing { get; }
    }
}