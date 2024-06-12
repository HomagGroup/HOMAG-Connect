using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Contracts.Common;
using HomagConnect.IntelliDivide.Samples.Requests.Cutting;
using HomagConnect.IntelliDivide.Samples.Requests.ObjectModel.Cutting;
using HomagConnect.IntelliDivide.Samples.Requests.Template.Cutting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Cutting;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Cutting")]
public class CuttingOptimizationRequestTests : IntelliDivideTestBase
{
#pragma warning disable S2699 // Tests should include assertions

    [TestInitialize]
    public async Task Initialize()
    {
        await EnsureSampleMaterialExists(CuttingRequestUsingObjectModelSamples.SampleMaterialCodes.Union(CuttingRequestUsingTemplateSamples.SampleMaterialCodes));
        await WaitForParallelRunningOptimizationsWithinLimit(OptimizationType.Cutting, TimeSpan.FromMinutes(2));
    }

    [TestMethod]
    public async Task CuttingRequest_ObjectModel_RequiredProperties_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_RequiredProperties_ImportOnly(intelliDivide);
    }

    [TestMethod]
    public async Task CuttingRequest_ObjectModel_TypicalProperties_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_TypicalProperties_ImportOnly(intelliDivide);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2024,7,1)]
    public async Task CuttingRequest_ObjectModel_GrainMatchingTemplate_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingObjectModelSamples.CuttingRequest_ObjectModel_GrainMatchingTemplate_ImportOnly(intelliDivide);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2024, 6, 15)]
    public async Task CuttingRequest_Template_Excel_ImportOnly()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingTemplateSamples.CuttingRequest_Template_Excel_ImportOnly(intelliDivide);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2024,6,15)]
    public async Task CuttingRequest_Template_Excel_ImportAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingRequestUsingTemplateSamples.CuttingRequest_Template_Excel_ImportAndOptimize(intelliDivide);
    }

    [TestMethod]
    public async Task CreatedCuttingOptimizationUsingProjectZip()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingProjectZip.CreatedCuttingOptimizationUsingProjectZip(intelliDivide);
    }

    [TestMethod]
    [TestCategory("UserTestInteractionNeeded")]
    public async Task CreateCuttingOptimizationUsingModel()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModel(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationByObjectModelWithSpecificBoards()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelWithSpecificBoards(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationByObjectModelValidationResults()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelValidationResults(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationUsingModelAndOptimize()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelAndOptimize(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationByObjectModelOptimizeAndRetrieveResults()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelOptimizeAndRetrieveResults(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationUsingModelAndOptimizeAndSend()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelAndOptimizeAndSend(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationByObjectModelOptimizeAndArchive()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelOptimizeAndArchive(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationByObjectModelAndDelete()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelAndDelete(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationByObjectModelAndStartOptimization()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelAndStartOptimization(intelliDivide);
    }

    [TestMethod]
    public async Task CreateCuttingOptimizationByObjectModelAndSendSolution()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelAndSendSolution(intelliDivide);
    }

#pragma warning restore S2699 // Tests should include assertions
}