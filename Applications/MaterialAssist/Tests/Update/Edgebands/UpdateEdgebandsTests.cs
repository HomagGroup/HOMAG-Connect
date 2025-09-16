using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Update.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Update.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class UpdateEdgebandsTests : MaterialAssistTestBase
    {
        /* feature not ready yet
          
         
        [ClassInitialize]
        public static async Task Initialie(TestContext testContext)
        {
            var classInstance = new UpdateEdgebandsTests();
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
            catch { }

            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "43",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "44",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "45",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
        }

        [TestMethod]
        public async Task EdgebandsRemoveAllEdgebandEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_RemoveAllEdgebandEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsRemoveSingleEdgebandEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_RemoveSingleEdgebandEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsRemoveSubsetEdgebandEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_RemoveSubsetEdgebandEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsStoreEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_StoreEdgebandEntity(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsUpdateEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_UpdateEdgebandEntity(MaterialAssistClient);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new UpdateEdgebandsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            await MaterialAssistClient.DeleteEdgebandEntity(["43", "44", "45"]);

            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Edgebands;
            await MaterialManagerClient.DeleteEdgebandType("Test_Data_ABS_White_1mm");
        }
        */
    }
}