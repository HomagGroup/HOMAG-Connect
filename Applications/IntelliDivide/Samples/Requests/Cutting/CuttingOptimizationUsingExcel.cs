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
    public static async Task CreatedCuttingOptimizationByImportingFromExcel(IIntelliDivideClient intelliDivide)
    {
        var excelFile = new FileInfo(@"Requests\Cutting\Kitchen.xlsx");

        Assert.IsTrue(excelFile.Exists);

        var importFile = await ImportFile.CreateAsync(excelFile);

        var optimizationMachine = await intelliDivide.GetMachinesAsync(OptimizationType.Cutting).FirstAsync(m => m.Name == "productionAssist Cutting");
        var optimizationParameter = await intelliDivide.GetParametersAsync(optimizationMachine.OptimizationType).FirstAsync();
        var importTemplate = await intelliDivide.GetImportTemplatesAsync(optimizationMachine.OptimizationType, excelFile.Extension).FirstAsync(i => i.Name.Contains("homag.cloud"));

        var request = new OptimizationRequestUsingTemplate()
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