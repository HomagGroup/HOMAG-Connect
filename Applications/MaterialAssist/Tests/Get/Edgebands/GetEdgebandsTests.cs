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
            await classInstance.EnsureEdgebandTypeExists("Test_Data_ABS_White_1mm");

            // TODO: Ensure entity exists
            var materialAssistClient = classInstance.GetMaterialAssistClient().Edgebands;
            try
            {
                var edgebandEntityRequest = new MaterialAssistRequestEdgebandEntity()
                {
                    Id = "33",
                    EdgebandCode = "Test_Data_ABS_White_1mm",
                    ManagementType = ManagementType.Single,
                    Quantity = 1,
                    Length = 50,
                    CurrentThickness = 1.0
                };
                await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest);
            }
            catch { }
            try
            {
                var edgebandEntityRequest2 = new MaterialAssistRequestEdgebandEntity()
                {
                    Id = "34",
                    EdgebandCode = "Test_Data_ABS_White_1mm",
                    ManagementType = ManagementType.Single,
                    Quantity = 1,
                    Length = 50,
                    CurrentThickness = 1.0
                };
                await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest2);
            }
            catch { }
            try
            {
                var edgebandEntityRequest3 = new MaterialAssistRequestEdgebandEntity()
                {
                    Id = "35",
                    EdgebandCode = "Test_Data_ABS_White_1mm",
                    ManagementType = ManagementType.Single,
                    Quantity = 1,
                    Length = 50,
                    CurrentThickness = 1.0
                };
                await materialAssistClient.CreateEdgebandEntity(edgebandEntityRequest3);
            }
            catch { }
        }
           
        [TestMethod]
        public async Task EdgebandsGetEdgebandEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntities(materialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntityById()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntityById(materialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByIds()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByIds(materialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByEdgebandCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCode(materialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByEdgebandCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCodes(materialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetStorageLocations()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetStorageLocations(materialAssistClient);
        }

        [TestMethod]
        public async Task EdgebandsGetWorkstations()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            await GetEndgebandEntitiesSamples.Edgebands_GetWorkstations(materialAssistClient);
        }
        // TODO: Add asserts
    }
}
