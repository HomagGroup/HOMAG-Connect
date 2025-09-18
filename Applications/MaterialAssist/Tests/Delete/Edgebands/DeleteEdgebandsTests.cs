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
            await classInstance.EnsureEdgebandTypeExists("Test_Data_ABS_White_1mm");

            // TODO: Ensure entity exists
            var materialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "23",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "24",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "25",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
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
