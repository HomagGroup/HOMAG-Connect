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
            await classInstance.EnsureEdgebandTypeExist("ABS_White_2mm", 2, 23);
        }

        [TestMethod]
        public async Task EdgebandsUpdateEdgebandType()
        {
            Random random = new Random();
            double RandomBetween(double min, double max)
            {
                return random.NextDouble() * (max - min) + min;
            }
            double value = Math.Round(RandomBetween(50.0, 100.0), 2);

            var materialManagerClient = GetMaterialManagerClient();
            var edgebandCode = "ABS_White_2mm";
            await UpdateEdgebandTypeSamples.Edgebands_UpdateEdgebandType(materialManagerClient.Material.Edgebands, edgebandCode, value);

            var checkEdgeband = await materialManagerClient.Material.Edgebands.GetEdgebandTypeByEdgebandCode(edgebandCode);
            Assert.AreEqual(value, checkEdgeband.DefaultLength);
        }
    }
}
