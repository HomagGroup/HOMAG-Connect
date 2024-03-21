using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client
{
    public class MaterialManagerClient : ServiceBase
    {
        public MaterialManagerClient(HttpClient client) : base(client)
        {
            Processing = new MaterialManagerClientProcessing(client);
            Material = new MaterialManagerClientMaterial(client);
        }

        public MaterialManagerClientMaterial Material { get; set; }

        public MaterialManagerClientProcessing Processing { get; }
    }
}