using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;
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

            var edgebandEntity = await materialAssistClient.GetEdgebandEntityById("16");
            Assert.AreEqual("16", edgebandEntity.Id);
            Assert.AreEqual(ManagementType.Single, edgebandEntity.ManagementType);
            Assert.AreEqual(1, edgebandEntity.Quantity);
        }
        
        [TestCleanup]
        public async Task Cleanup()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await materialAssistClient.DeleteEdgebandEntity("16");
        }
    }
}
