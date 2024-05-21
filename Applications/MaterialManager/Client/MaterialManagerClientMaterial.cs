using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// 
/// </summary>
public class MaterialManagerClientMaterial : ServiceBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public MaterialManagerClientMaterial(HttpClient client) : base(client)
    {
        Boards = new MaterialManagerClientMaterialBoards(client);
        Edgebands = new MaterialManagerClientMaterialEdgebands(client);
    }

    /// <summary>
    /// 
    /// </summary>
    public MaterialManagerClientMaterialEdgebands Edgebands { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public MaterialManagerClientMaterialBoards Boards { get; set; }
}