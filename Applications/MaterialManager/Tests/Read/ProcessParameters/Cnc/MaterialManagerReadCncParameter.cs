using HomagConnect.MaterialManager.Samples.Read.ProcessParameters.Cnc;

namespace HomagConnect.MaterialManager.Tests.Read.ProcessParameters.Cnc
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Cnc.Read.Parameters")]
    public class MaterialManagerReadCncParameter : MaterialManagerTestBase
    {
#pragma warning disable S2699 // Tests should include assertions

        /// <summary />
        [TestMethod]
        public async Task GetMillingParameterToolGroups_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();
            await MaterialManagerReadCncParameters.GetMillingParameterToolGroups(materialManager.Processing.Cnc);
        }

        /// <summary />
        [TestMethod]
        public async Task GetMillingParameterMaterialGroups_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();
            await MaterialManagerReadCncParameters.GetMillingParameterMaterialGroups(materialManager.Processing.Cnc);
        }

        /// <summary />
        [TestMethod]
        public async Task GetMillingParameterGroups_GetResult_NoException()
        {
            var materialManager = GetMaterialManagerClient();
            await MaterialManagerReadCncParameters.GetMillingParameterGroups(materialManager.Processing.Cnc);
        }

#pragma warning restore S2699 // Tests should include assertions
    }
}