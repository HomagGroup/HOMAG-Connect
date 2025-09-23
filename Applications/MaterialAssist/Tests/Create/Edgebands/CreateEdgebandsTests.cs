using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Create.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class CreateEdgebandsTests : MaterialAssistTestBase
    {
        [TestInitialize]
        public async Task Initialize()
        {
            // TODO: use valid names
            await EnsureEdgebandTypeExist("Test_Data_ABS_White_1mm");
        }

        [TestMethod]
        public async Task EdgebandsCreateEdgebandEntity()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(materialAssistClient, "16");
        }
        
        [TestCleanup]
        public async Task Cleanup()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await materialAssistClient.DeleteEdgebandEntity("16");
        }
    }
}
