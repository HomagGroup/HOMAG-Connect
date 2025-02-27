using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.IntelliDivide.Samples.Materials;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Materials
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [TestCategory("IntelliDivide.Materials")]
    [TemporaryDisabledOnServer(2025, 3, 1, "DF-Optimization")] // Not yet on production server
    public class MaterialsTests : IntelliDivideTestBase
    {
        [TestMethod]
        public async Task Materials_GetBoardTypes()

        {
            var exceptionThrown = false;

            try
            {
                var intelliDivide = GetIntelliDivideClient();
                await MaterialsSamples.Materials_GetBoardTypes(intelliDivide);
            }
            catch (Exception e)
            {
                e.Trace();
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod]
        public async Task Materials_GetEdgebandTypes()

        {
            var exceptionThrown = false;

            try
            {
                var intelliDivide = GetIntelliDivideClient();
                await MaterialsSamples.Materials_GetEdgebandTypes(intelliDivide);
            }
            catch (Exception e)
            {
                e.Trace();
                exceptionThrown = true;
            }

            Assert.IsFalse(exceptionThrown);
        }
    }
}