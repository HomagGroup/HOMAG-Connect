using HomagConnect.Base.Tests.Attributes;
using HomagConnect.IntelliDivide.Samples.Requests.Nesting;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Requests.Nesting
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [TestCategory("IntelliDivide.Requests.Nesting")]
    public class NestingOptimizationRequestTests : IntelliDivideTestBase
    {
        [TestMethod]
        [TemporaryDisabledOnServer(2024, 5, 1)]
        public async Task CreateNestingOptimizationUsingProjectZip()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZip(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 5, 1)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimize()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimize(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 5, 1)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend(intelliDivide);
        }
    }
}
