using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client;

public class MaterialManagerClientProcessing : ServiceBase
{
    public MaterialManagerClientProcessing(HttpClient client) : base(client)
    {
        Optimization = new MaterialManagerClientProcessingOptimization(client);
    }

    public MaterialManagerClientProcessingOptimization Optimization { get; }

    
}