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
            await classInstance.EnsureEdgebandTypeExist("Test_Data_ABS_White_2mm", 2, 23);
        }

        [TestMethod]
        public async Task EdgebandsUpdateEdgebandType()
        {
            Random random = new Random();
            double RandomBetween(double min, double max)
            {
                return random.NextDouble() * (max - min) + min;
            }
            double value = RandomBetween(50.0, 100.0);

            var materialManagerClient = GetMaterialManagerClient();
            var edgebandCode = "Test_Data_ABS_White_2mm";
            await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType(materialManagerClient.Material.Edgebands, edgebandCode, value);

            var checkEdgeband = await materialManagerClient.Material.Edgebands.GetEdgebandTypeByEdgebandCode(edgebandCode);
            Assert.AreEqual(value, checkEdgeband.DefaultLength);
        }
    }
}
