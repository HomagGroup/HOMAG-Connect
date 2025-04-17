using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Processing.Interfaces;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager Client for processing optimization
/// </summary>
public class MaterialManagerClientProcessingOptimization : ServiceBase, IMaterialManagerClientProcessingOptimization
{
    #region Constants

    private const string _BaseRoute = "api/materialManager/processing/optimization";
    private const string _Offcuts = "offcuts";
    private const string _MaterialCode = "materialCode";

    #endregion Constants

    /// <inheritdoc />
    public MaterialManagerClientProcessingOptimization(HttpClient client) : base(client) { }

    #region Offcuts

    /// <inheritdoc />
    public async Task<OffcutParameterSet> GetOffcutParameterSetAsync(string materialCode)
    {
        var offcutParameterSets = await GetOffcutParameterSetsAsync(new List<string> { materialCode }).ConfigureAwait(false);

        return offcutParameterSets.First();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<OffcutParameterSet>?> GetOffcutParameterSetsAsync(ICollection<string> materialCodes)
    {
        var validatedMaterialCodes = materialCodes.Select(m => m.Trim()).Where(m => !string.IsNullOrWhiteSpace(m)).Distinct()
            .OrderBy(m => m).ToArray();

        StringBuilder materialCodesString = new StringBuilder();
        materialCodesString.Append(string.Join("&", validatedMaterialCodes.Select(materialCode => $"{_MaterialCode}={materialCode}")));

        var url = $"{_BaseRoute}/{_Offcuts}?{materialCodesString}";

        return await RequestEnumerable<OffcutParameterSet>(new Uri(url, UriKind.Relative));
    }

    #endregion Offcuts
}