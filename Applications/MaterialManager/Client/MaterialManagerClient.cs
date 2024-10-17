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
            Processing = new MaterialManagerClientProcessing(client);
            Material = new MaterialManagerClientMaterial(client);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public MaterialManagerClientMaterial Material { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MaterialManagerClientProcessing Processing { get; }
    }
}