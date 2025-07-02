using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Processing.Cnc;
using HomagConnect.MaterialManager.Contracts.Processing.Interfaces;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager Client for processing CNC
/// </summary>
public class MaterialManagerClientProcessingCnc : ServiceBase, IMaterialManagerClientProcessingCnc
{
    #region Constants

    private const string _BaseCncRoute = "api/materialManager/processing/cnc/milling";

    #endregion Constants

    /// <inheritdoc />
    public MaterialManagerClientProcessingCnc(HttpClient client) : base(client) { }

    #region Milling

    /// <summary>
    /// Gets the milling parameter tool groups for the current subscription.
    /// </summary>
    public async Task<IEnumerable<ToolGroup>?> GetMillingParameterToolGroups()
    {
        var url = $"{_BaseCncRoute}/toolGroups";
        return await RequestEnumerable<ToolGroup>(new Uri(url, UriKind.Relative));
    }

    /// <summary>
    /// Gets the milling parameter material groups for the current subscription.
    /// </summary>
    public async Task<IEnumerable<MaterialGroup>?> GetMillingParameterMaterialGroups()
    {
        var url = $"{_BaseCncRoute}/materialGroups";
        return await RequestEnumerable<MaterialGroup>(new Uri(url, UriKind.Relative));
    }

    /// <summary>
    /// Gets the milling parameter parameter groups for the current subscription.
    /// </summary>
    public async Task<IEnumerable<MillingParameterGroup>?> GetMillingParameterGroups()
    {
        var url = $"{_BaseCncRoute}/parameterGroups";
        return await RequestEnumerable<MillingParameterGroup>(new Uri(url, UriKind.Relative));
    }

    #endregion Milling
}