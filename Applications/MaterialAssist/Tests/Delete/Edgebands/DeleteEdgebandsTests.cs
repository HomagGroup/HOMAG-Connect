using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

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
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Edgebands;

            try
            {
                var edgebandTypeRequest = new MaterialManagerRequestEdgebandType
                {
                    EdgebandCode = "Test_Data_ABS_White_1mm",
                    Height = 20,
                    Thickness = 1.0,
                    DefaultLength = 23.0,
                    MaterialCategory = EdgebandMaterialCategory.Veneer,
                    Process = EdgebandingProcess.Other,
                };
                await MaterialManagerClient.CreateEdgebandType(edgebandTypeRequest);
            }
            catch{ }

            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "23",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "24",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "25",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
        }

        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(MaterialAssistClient, "23");
        }

        [TestMethod]
        public async Task EdgebandsDeleteEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntities(MaterialAssistClient, ["24", "25"]);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new DeleteEdgebandsTests();
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Edgebands;
            await MaterialManagerClient.DeleteEdgebandType("Test_Data_ABS_White_1mm");
        }
    }
}
