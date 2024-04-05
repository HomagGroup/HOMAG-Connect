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
                Parameters = new OffcutParameters
                {
                    OffcutsEnabled = true,
                    OffcutMinimumLength = 500,
                    OffcutMinimumWidth = 500,
                    OffcutMinimumArea = 0.3,
                    OffcutValue = 0.3,
                    LargeOffcutsEnabled = true,
                    LargeOffcutMinimumLength = 1500,
                    LargeOffcutMinimumWidth = 800,
                    LargeOffcutMinimumArea = 1.6,
                    LargeOffcutValue = 0.8
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
                Parameters = new OffcutParameters
                {
                    OffcutsEnabled = true,
                    OffcutMinimumLength = 400,
                    OffcutMinimumWidth = 400,
                    OffcutMinimumArea = 0.2,
                    OffcutValue = 0.8,
                    LargeOffcutsEnabled = false
                }
            });
        }

        var materialCodesMdf = validatedMaterialCodes.Where(m => m.StartsWith("MDF_")).ToArray();

        if (materialCodesMdf.Any())
        {
            offcutParameterSets.Add(new OffcutParameterSet
            {
                MaterialGroupName = "Cheap (do not waste the time)",
                MaterialCodes = materialCodesMdf,
                Parameters = new OffcutParameters
                {
                    OffcutsEnabled = false,
                    LargeOffcutsEnabled = false
                }
            });
        }

        return offcutParameterSets;

        return await RequestEnumerable<OffcutParameterSet>(new Uri(url, UriKind.Relative));
    }
}