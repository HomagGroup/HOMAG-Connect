using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client;

public class MaterialManagerClientMaterial : ServiceBase
{
    public MaterialManagerClientMaterial(HttpClient client) : base(client)
    {
        Boards = new MaterialManagerClientMaterialBoards(client);
        Edgebands = new MaterialManagerClientMaterialEdgebands(client);
    }

    public MaterialManagerClientMaterialEdgebands Edgebands { get; set; }

    public MaterialManagerClientMaterialBoards Boards { get; set; }
}