using Shouldly;

using HomagConnect.Base.Extensions;
using HomagConnect.IntelliDivide.Samples.Materials;
using HomagConnect.IntelliDivide.Tests.Base;

namespace HomagConnect.IntelliDivide.Tests.Materials
{
    [TestClass]
    [TestCategory("IntelliDivide")]
    [TestCategory("IntelliDivide.Materials")]
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

            exceptionThrown.ShouldBeFalse("The call for materials board types should be successful.");
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

            exceptionThrown.ShouldBeFalse("The call for material edge band types should be successful.");
        }
    }
}