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
        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await MaterialAssistClient.DeleteEdgebandEntity(["42", "50", "23"]);
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

        [ClassInitialize]
        public async Task Initialie()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "42",
                EdgebandCode = "White Edgeband 19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "50",
                EdgebandCode = "White Edgeband 19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "23",
                EdgebandCode = "White Edgeband 19mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 1000,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
        }
    }
}