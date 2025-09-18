using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Update.Edgebands;

namespace HomagConnect.MaterialManager.Tests.Update.Edgebands
{
    /// <summary />
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Edgebands")]
    public class UpdateEdgebandTypeTests : MaterialManagerTestBase
    {
        /// <summary />
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new UpdateEdgebandTypeTests();
            await classInstance.EnsureEdgebandTypeExist("Test_Data_ABS_White_2mm", 1, 23);
        }

        [TestMethod]
        public async Task EdgebandsUpdateEdgebandType()
        {
            var materialManagerClient = GetMaterialManagerClient();
            var edgebandCode = "Test_Data_ABS_White_2mm";
            await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType(materialManagerClient.Material.Edgebands, edgebandCode);
        }
        
        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new UpdateEdgebandTypeTests();
            var materialManagerClient = classInstance.GetMaterialManagerClient();
            await materialManagerClient.Material.Edgebands.DeleteEdgebandType("Test_Data_ABS_White_2mm");
        }
    }
}
