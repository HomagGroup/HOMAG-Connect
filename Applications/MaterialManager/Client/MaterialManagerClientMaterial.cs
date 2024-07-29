using System;
using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// 
/// </summary>
public class MaterialManagerClientMaterial : ServiceBase
{
    #region Constructors

    /// <inheritdoc />
    public MaterialManagerClientMaterial(HttpClient client) : base(client)
    {
        Boards = new MaterialManagerClientMaterialBoards(client);
        Edgebands = new MaterialManagerClientMaterialEdgebands(client);
    }

    /// <inheritdoc />
    public MaterialManagerClientMaterial(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey)
    {
        Boards = new MaterialManagerClientMaterialBoards(subscriptionOrPartnerId, authorizationKey);
        Edgebands = new MaterialManagerClientMaterialEdgebands(subscriptionOrPartnerId, authorizationKey);
    }

    /// <inheritdoc />
    public MaterialManagerClientMaterial(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri)
    {
        Boards = new MaterialManagerClientMaterialBoards(subscriptionOrPartnerId, authorizationKey, baseUri);
        Edgebands = new MaterialManagerClientMaterialEdgebands(subscriptionOrPartnerId, authorizationKey, baseUri);
    }

    #endregion
    
    /// <summary>
    /// 
    /// </summary>
    public MaterialManagerClientMaterialEdgebands Edgebands { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public MaterialManagerClientMaterialBoards Boards { get; set; }
}