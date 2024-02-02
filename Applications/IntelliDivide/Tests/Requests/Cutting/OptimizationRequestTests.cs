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
}