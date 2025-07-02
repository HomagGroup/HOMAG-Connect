using System.Net.Http;

using HomagConnect.Base.Services;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// 
/// </summary>
public class MaterialManagerClientProcessing : ServiceBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public MaterialManagerClientProcessing(HttpClient client) : base(client)
    {
        Optimization = new MaterialManagerClientProcessingOptimization(client);
        Cnc = new MaterialManagerClientProcessingCnc(client);
    }

    /// <summary>
    /// 
    /// </summary>
    public MaterialManagerClientProcessingOptimization Optimization { get; }

    /// <summary>
    /// 
    /// </summary>
    public MaterialManagerClientProcessingCnc Cnc { get; }


}