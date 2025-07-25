using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using HomagConnect.MaterialAssist.Samples.Get.Edgebands;
using HomagConnect.MaterialAssist.Samples.Update.Edgebands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Update.Edgebands
{
    public class UpdateEdgebandsTests : MaterialAssistTestBase
    {

        [ClassInitialize]
        public async Task Initialize()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "42");
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "50");
            await CreateEdgebandEntitiesSamples.Edgebands_CreateEdgebandEntity(MaterialAssistClient, "23");
        }

        [TestMethod]
        public async Task EdgebandsUpdateEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_UpdateEdgebandEntities(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsStoreEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_StoreEdgebandEntities(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsRemoveAllEdgebandEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_RemoveAllEdgebandEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsRemoveSubsetEdgebandEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_RemoveSubsetEdgebandEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsRemoveSingleEdgebandEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await UpdateEdgebandEntitiesSamples.Edgebands_RemoveSingleEdgebandEntitiesFromWorkplace(MaterialAssistClient);
        }

        [ClassCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(MaterialAssistClient, "42");
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(MaterialAssistClient, "50");
            await DeleteEdgebandEntitiesSamples.Edgebands_DeleteEdgebandEntity(MaterialAssistClient, "23");
        }
    }
}
