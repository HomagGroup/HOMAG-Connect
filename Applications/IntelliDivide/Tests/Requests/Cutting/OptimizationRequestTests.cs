using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Samples.Requests.Cutting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Cutting;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Cutting")]
public class CuttingOptimizationRequestTests : IntelliDivideTestBase
{
    [TestMethod]
    [TemporaryDisabledOnServer(2024,5,1)]
    public async Task CreatedCuttingOptimizationBasedOnExcelFile()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingExcel.CreatedCuttingOptimizationByImportingFromExcel(intelliDivide).ConfigureAwait(false);
    }

    [TestMethod]
    [TemporaryDisabledOnServer(2024, 5, 1)]
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
}