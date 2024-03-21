using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client;

public class MaterialManagerClientMaterial : ServiceBase
{
    public MaterialManagerClientMaterial(HttpClient client) : base(client)
    {
        Boards = new MaterialManagerClientMaterialBoards(client);
    }

    public MaterialManagerClientMaterialBoards Boards { get; set; }
}