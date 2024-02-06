using HomagConnect.IntelliDivide.Client;
using HomagConnect.IntelliDivide.Contracts;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Contracts.Request;
using HomagConnect.IntelliDivide.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.IntelliDivide.Samples.Requests.Cutting;

/// <summary />
public class CuttingOptimizationUsingExcel
{
    /// <summary />
    public static async Task CreatedCuttingOptimizationByImportingFromExcel(IntelliDivideClient intelliDivide)
    {
        var excelFile = new FileInfo(@"Requests\Cutting\Kitchen.xlsx");

        Assert.IsTrue(excelFile.Exists);

        var importFile = await ImportFile.CreateAsync(excelFile);

        var optimizationMachine = (await intelliDivide.GetMachinesAsync(OptimizationType.Cutting)).First(m => m.Name == "productionAssist Cutting");
        var optimizationParameter = (await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType)).First(p => p.Name == "Default");
        var importTemplate = (await intelliDivide.GetImportTemplatesAsync(optimizationMachine.OptimizationType, excelFile.Extension)).First(i => i.Name.Contains("homag.cloud"));

        var request = new OptimizationRequest
        {
            Name = "Connect " + excelFile.Name + " " + DateTime.Now.ToString("s"),
            Machine = optimizationMachine.Name,
            Parameters = optimizationParameter.Name,
            ImportTemplate = importTemplate.Name,
            Action = OptimizationRequestAction.ImportOnly
        };

        var response = await intelliDivide.RequestOptimizationAsync(request, importFile);

        Assert.IsNotNull(response);
        Assert.IsNotNull(response.OptimizationId);
        Assert.AreEqual(OptimizationStatus.New, response.OptimizationStatus);
        Assert.IsFalse(response.ValidationErrors.Any());

        response.Trace();

        var optimization = await intelliDivide.GetOptimizationAsync(response.OptimizationId);

        Assert.IsNotNull(optimization);
        Assert.AreEqual(OptimizationStatus.New, optimization.Status);
        Assert.AreEqual(optimizationMachine.Name, optimization.Machine);
        Assert.AreEqual(optimizationParameter.Name, optimization.ParameterName);
        Assert.AreEqual(69, optimization.QuantityOfParts);
    }
}