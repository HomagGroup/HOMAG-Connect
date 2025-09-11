using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Get.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Get.Edgebands
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Edgebands")]
    public class GetEdgebandsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new GetEdgebandsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;

            var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "33",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);

            var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "34",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);

            var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
            {
                Id = "35",
                EdgebandCode = "Test_Data_ABS_White_1mm",
                ManagementType = ManagementType.Single,
                Quantity = 1,
                Length = 50,
                CurrentThickness = 1.0
            };
            await MaterialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
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
        public static async Task Cleanup()
        {
            var classInstance = new GetEdgebandsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            await MaterialAssistClient.DeleteEdgebandEntity(["33", "34", "35"]);
        }
    }
}
