using HomagConnect.IntelliDivide.Samples.Requests.Cutting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Cutting;

[TestClass]
[TestCategory("IntelliDivide")]
[TestCategory("IntelliDivide.Requests.Cutting")]
[TestCategory("UserTestInteractionNeeded")]
public class CuttingOptimizationRequestTests : IntelliDivideTestBase
{
    [TestMethod]
    public async Task CreatedCuttingOptimizationBasedOnExcelFile()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingExcel.CreatedCuttingOptimizationByImportingFromExcel(intelliDivide).ConfigureAwait(false);
    }

    [TestMethod]
    public async Task CreatedCuttingOptimizationUsingProjectZip()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingProjectZip.CreatedCuttingOptimizationUsingProjectZip(intelliDivide);
    }
    
    [TestMethod]
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
    public async Task CreateCuttingOptimizationUsingModelAndOptimizeAndSend()
    {
        var intelliDivide = GetIntelliDivideClient();

        await CuttingOptimizationUsingObjectModel.CreateCuttingOptimizationByObjectModelAndOptimizeAndSend(intelliDivide);
    }
}