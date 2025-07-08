using System;
using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// </summary>
public class MaterialManagerClientProcessing : ServiceBase
{
    /// <inheritdoc />
    public MaterialManagerClientProcessing(HttpClient client) : base(client)
    {
        Optimization = new MaterialManagerClientProcessingOptimization(client);
        Cnc = new MaterialManagerClientProcessingCnc(client);
    }

    /// <inheritdoc />
    public MaterialManagerClientProcessing(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey)
    {
        Optimization = new MaterialManagerClientProcessingOptimization(subscriptionOrPartnerId, authorizationKey);
        Cnc = new MaterialManagerClientProcessingCnc(subscriptionOrPartnerId, authorizationKey);
    }

    /// <inheritdoc />
    public MaterialManagerClientProcessing(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri)
    {
        Optimization = new MaterialManagerClientProcessingOptimization(subscriptionOrPartnerId, authorizationKey, baseUri);
        Cnc = new MaterialManagerClientProcessingCnc(subscriptionOrPartnerId, authorizationKey, baseUri);
    }

    /// <summary>
    /// Client for managing CNC processing.
    /// </summary>
    public MaterialManagerClientProcessingCnc Cnc { get; }

    /// <summary>
    /// Client for managing optimization processing.
    /// </summary>
    public MaterialManagerClientProcessingOptimization Optimization { get; }
}