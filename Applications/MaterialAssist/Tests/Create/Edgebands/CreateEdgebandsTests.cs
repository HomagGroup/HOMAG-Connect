using HomagConnect.MaterialAssist.Samples.Create.Edgebands;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class CreateEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            var test = new CreateEdgebandsTests();
            var MaterialManagerClient = test.GetMaterialManagerClient().Material.Edgebands;
            Assert.IsNotNull(await MaterialManagerClient.GetEdgebandTypeByEdgebandCode("White Edgeband 19mm"));
            Assert.IsNull(await MaterialManagerClient.GetEdgebandTypeByEdgebandCode("White Edgeband 1mm"));
        }

        [TestMethod]
        public async Task EdgebandsCreateEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "42");
            Assert.IsNotNull(await MaterialAssistClient.GetEdgebandEntityById("42"));
        }
        
        [TestMethod]
        public async Task EdgebandsCreateEdgebandType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            var MaterialManagerClient = GetMaterialManagerClient().Material.Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandType(MaterialAssistClient, "White Edgeband 1mm");
            Assert.IsNotNull(await MaterialManagerClient.GetEdgebandTypeByEdgebandCode("White Edgeband 1mm"));
        }

        [ClassCleanup]
        public static async Task Cleanup ()
        {
            var test = new CreateEdgebandsTests();
            var MaterialAssistClient = test.GetMaterialAssistClient().Edgebands;
            var MaterialManagerClient = test.GetMaterialManagerClient();

            await MaterialAssistClient.DeleteEdgebandEntity("42");
            Assert.IsNull(await MaterialAssistClient.GetEdgebandEntityById("42"));
            await MaterialManagerClient.Material.Edgebands.DeleteEdgebandType("White Edgeband 1mm");
            Assert.IsNull(await MaterialManagerClient.Material.Edgebands.GetEdgebandTypeByEdgebandCode("White Edgeband 1mm"));
        }
    }
}
