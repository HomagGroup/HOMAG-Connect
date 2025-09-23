using HomagConnect.MaterialAssist.Samples.Update.Edgebands;

namespace HomagConnect.MaterialAssist.Tests.Update.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class UpdateEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new UpdateEdgebandsTests();
            await classInstance.EnsureEdgebandTypeExist("ABS_White_1mm");
            await classInstance.EnsureEdgebandEntityExist("43", "ABS_White_1mm");
        }
        
        [TestMethod]
        public async Task EdgebandsUpdateEdgebandEntities()
        {
            Random random = new Random();
            double RandomBetween(double min, double max)
            {
                return random.NextDouble() * (max - min) + min;
            }
            double length = RandomBetween(50.0, 100.0);

            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_UpdateEdgebandEntity(materialAssistClient, length);

            var checkEdgebandEntity = await materialAssistClient.GetEdgebandEntityById("43");
            Assert.AreEqual(length, checkEdgebandEntity.Length);
        }
    }
}