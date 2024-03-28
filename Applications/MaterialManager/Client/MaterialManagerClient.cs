using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client
{
    public class MaterialManagerClient(HttpClient client) : ServiceBase(client)
    {
        public MaterialManagerClientMaterial Material { get; set; } = new MaterialManagerClientMaterial(client);

        public MaterialManagerClientProcessing Processing { get; } = new MaterialManagerClientProcessing(client);
    }
}