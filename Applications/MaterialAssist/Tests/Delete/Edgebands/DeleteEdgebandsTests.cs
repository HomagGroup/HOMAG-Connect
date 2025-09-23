using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Delete.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class DeleteEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialie(TestContext testContext)
        {
            var classInstance = new DeleteEdgebandsTests();
            await classInstance.EnsureEdgebandTypeExist("ABS_White_1mm");

            await classInstance.EnsureEdgebandEntityExist("23", "ABS_White_1mm");
            await classInstance.EnsureEdgebandEntityExist("24", "ABS_White_1mm");
            await classInstance.EnsureEdgebandEntityExist("25", "ABS_White_1mm");
        }

        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntity()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(materialAssistClient, "23");
        }

        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntities(materialAssistClient, ["24", "25"]);
        }
    }
}
