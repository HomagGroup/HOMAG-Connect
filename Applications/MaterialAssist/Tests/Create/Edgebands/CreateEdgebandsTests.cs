using HomagConnect.MaterialAssist.Samples.Create.Edgebands;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class CreateEdgebandsTests : MaterialAssistTestBase
    {
        /*
        [TestMethod]
        public async Task EdgebandsCreateEdgebandType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandType(MaterialAssistClient, "ABS_White_1mm");
        }
        */

        [TestMethod]
        public async Task EdgebandsCreateEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "42");
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new CreateEdgebandsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            await MaterialAssistClient.DeleteEdgebandEntity("42");
        }
    }
}
