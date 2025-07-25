using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialAssist.Samples.Create.Edgebands;
using HomagConnect.MaterialAssist.Samples.Delete.Edgebands;
using HomagConnect.MaterialAssist.Samples.Get.Boards;
using HomagConnect.MaterialAssist.Samples.Get.Edgebands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Get.Edgebands
{
    public class GetEdgebandsTests : MaterialAssistTestBase
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
        public async Task EdgebandsGetEdgebandEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntities(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntityById()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntityById(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByIds()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByIds(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByEdgebandCode()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCode(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByEdgebandCodes()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCodes(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetStorageLocations()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetStorageLocations(MaterialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetWorkstations()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetWorkstations(MaterialAssistClient);
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
