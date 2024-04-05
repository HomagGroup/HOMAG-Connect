using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Processing.Optimization;

namespace HomagConnect.MaterialManager.Client;

public class MaterialManagerClientProcessingOptimization : ServiceBase
{
    public MaterialManagerClientProcessingOptimization(HttpClient client) : base(client) { }

    public async Task<OffcutParameterSet> GetOffcutParameterSetAsync(string materialCode)
    {
        var offcutParameterSets = await GetOffcutParameterSetsAsync(new[] { materialCode });

        return offcutParameterSets.First();
    }

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

        var materialCodesP2 = validatedMaterialCodes.Where(m => m.StartsWith("P2_")).ToArray();

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

        var materialCodesVp = validatedMaterialCodes.Where(m => m.StartsWith("VP_")).ToArray();

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

        var materialCodesMdf = validatedMaterialCodes.Where(m => m.StartsWith("MDF_")).ToArray();

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

        return offcutParameterSets;

        return await RequestEnumerable<OffcutParameterSet>(new Uri(url, UriKind.Relative));
    }
}