using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Update.Edgebands;
using HomagConnect.MaterialAssist.Tests.Update.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Update.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class UpdateEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialie(TestContext testContext)
        {
            var classInstance = new UpdateEdgebandsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;

            var edgebandTypeRequest = new MaterialManagerRequestEdgebandType()
            {
                EdgebandCode = "ABS_White_5mm",
                Height = 20,
                Thickness = 5.0,
                DefaultLength = 500.0,
                MaterialCategory = EdgebandMaterialCategory.Veneer,
                Process = EdgebandingProcess.Other,
            };
            var newEdgebandEntity = await MaterialAssistClient.CreateEdgebandType(edgebandTypeRequest);

            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "42",
                EdgebandCode = "ABS_White_5mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 500,
                CurrentThickness = 5.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "50",
                EdgebandCode = "ABS_White_19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 500,
                CurrentThickness = 5.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "23",
                EdgebandCode = "ABS_White_19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 500,
                CurrentThickness = 5.0
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
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Edgebands;

            await MaterialAssistClient.DeleteEdgebandEntity(["42", "50", "23"]);
            await MaterialManagerClient.DeleteEdgebandType("ABS_White_5mm");
        }
    }
}