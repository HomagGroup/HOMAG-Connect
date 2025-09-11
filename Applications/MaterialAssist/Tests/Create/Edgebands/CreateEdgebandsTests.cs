using HomagConnect.MaterialAssist.Samples.Create.Edgebands;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class CreateEdgebandsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task EdgebandsCreateEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "16");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await MaterialAssistClient.DeleteEdgebandEntity("16");
        }
    }
}
