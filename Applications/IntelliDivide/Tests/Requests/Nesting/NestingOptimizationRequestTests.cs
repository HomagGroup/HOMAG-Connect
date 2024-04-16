using HomagConnect.IntelliDivide.Contracts.Request;
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
#pragma warning disable S2699 // Tests should include assertions

        [TestMethod]
        public async Task CreateNestingOptimizationRequestByObjectModel()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModel(intelliDivide);
        }

        [TestMethod]
        public async Task CreateNestingOptimizationRequestByObjectModelAndOptimize()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelAndOptimize(intelliDivide);
        }

        [TestMethod]
        public async Task CreateNestingOptimizationRequestByObjectModelAndOptimizeAndSend()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingObjectModel.CreateNestingOptimizationByObjectModelAndOptimizeAndSend(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 5, 1)]
        public async Task CreateNestingOptimizationUsingProjectZip()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZip(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 5, 1)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend(intelliDivide);
        }

        [TestMethod]
        [TemporaryDisabledOnServer(2024, 5, 1)]
        public async Task CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend()
        {
            var intelliDivide = GetIntelliDivideClient();

            await NestingOptimizationUsingProjectZip.CreateNestingOptimizationUsingProjectZipAndOptimizeAndSend(intelliDivide);
        }
    }

#pragma warning restore S2699 // Tests should include assertions
}