using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// 
/// </summary>
public class MaterialManagerClientProcessingOptimization : ServiceBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="client"></param>
    public MaterialManagerClientProcessingOptimization(HttpClient client) : base(client) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="materialCode"></param>
    /// <returns></returns>
    public async Task<OffcutParameterSet> GetOffcutParameterSetAsync(string materialCode)
    {
        var offcutParameterSets = await GetOffcutParameterSetsAsync([materialCode]);

        return offcutParameterSets.First();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="materialCodes"></param>
    /// <returns></returns>
    public async Task<IEnumerable<OffcutParameterSet>> GetOffcutParameterSetsAsync(IEnumerable<string> materialCodes)
    {
        var validatedMaterialCodes = materialCodes.Select(m => m.Trim()).Where(m => !string.IsNullOrWhiteSpace(m)).Distinct().OrderBy(m => m).ToArray();

        var queryParameters = new StringBuilder();

        foreach (var materialCode in validatedMaterialCodes)
        {
            queryParameters.Append($"materialCode={Uri.EscapeDataString(materialCode)}&");
        }

        var url = $"/api/materialManager/processing/optimization/offcuts?{queryParameters.ToString().Trim('&')}";

        var offcutParameterSets = new List<OffcutParameterSet>();

        // Mock the request for now

#pragma warning disable S109 // Magic numbers should not be used
#pragma warning disable S125 // Sections of code should not be commented out

        var materialCodesP2 = validatedMaterialCodes.Where(m => m.StartsWith("P2_", StringComparison.InvariantCultureIgnoreCase)).ToArray();

        if (materialCodesP2.Any())
        {
            offcutParameterSets.Add(new OffcutParameterSet
            {
                MaterialGroupName = "Chipboard",
                MaterialCodes = materialCodesP2,
                OffcutParameters = new OffcutParameters
                {
                    Enabled = true,
                    MinimumLength = 500,
                    MinimumWidth = 500,
                    MinimumArea = 0.3,
                    Value = 0.3,
                },
                LargeOffcutParameters = new OffcutParameters
                {
                    Enabled = true,
                    MinimumLength = 1500,
                    MinimumWidth = 800,
                    MinimumArea = 1.6,
                    Value = 0.8
                }
            });
        }

        var materialCodesVp = validatedMaterialCodes.Where(m => m.StartsWith("VP_", StringComparison.InvariantCultureIgnoreCase)).ToArray();

        if (materialCodesVp.Any())
        {
            offcutParameterSets.Add(new OffcutParameterSet
            {
                MaterialGroupName = "Expensive (we keep everything)",
                MaterialCodes = materialCodesVp,
                OffcutParameters = new OffcutParameters
                {
                    Enabled = true,
                    MinimumLength = 400,
                    MinimumWidth = 400,
                    MinimumArea = 0.2,
                    Value = 0.8,
                },
                LargeOffcutParameters = new OffcutParameters
                {
                    Enabled = false
                },
            });
        }

        var materialCodesMdf = validatedMaterialCodes.Where(m => m.StartsWith("MDF_", StringComparison.InvariantCultureIgnoreCase)).ToArray();

        if (materialCodesMdf.Any())
        {
            offcutParameterSets.Add(new OffcutParameterSet
            {
                MaterialGroupName = "Cheap (do not waste the time)",
                MaterialCodes = materialCodesMdf,
                OffcutParameters = new OffcutParameters
                {
                    Enabled = false
                },
                LargeOffcutParameters = new OffcutParameters
                {
                    Enabled = false
                }
            });
        }

        var materialCodesDefault = validatedMaterialCodes.Where(m => !offcutParameterSets.SelectMany(p => p.MaterialCodes).Contains(m)).ToArray();

        if (materialCodesDefault.Any())
        {
            offcutParameterSets.Add(new OffcutParameterSet
            {
                MaterialGroupName = "Default",
                MaterialCodes = materialCodesDefault,
                OffcutParameters = new OffcutParameters
                {
                    Enabled = true,
                    MinimumLength = 1200,
                    MinimumWidth = 1000,
                    MinimumArea = 0.3,
                    Value = 0.3,
                },
                LargeOffcutParameters = new OffcutParameters
                {
                    Enabled = true,
                    MinimumLength = 1500,
                    MinimumWidth = 1200,
                    MinimumArea = 1.6,
                    Value = 0.8
                }
            });
        }


        return await Task.FromResult(offcutParameterSets);

        // return await RequestEnumerable<OffcutParameterSet>(new Uri(url, UriKind.Relative));

        #pragma warning restore S125 // Sections of code should not be commented out
#pragma warning restore S109 // Magic numbers should not be used

    }

}