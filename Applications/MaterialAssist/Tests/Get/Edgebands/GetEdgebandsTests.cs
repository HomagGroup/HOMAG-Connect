using HomagConnect.MaterialAssist.Samples.Get.Edgebands;

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
            await classInstance.EnsureEdgebandTypeExist("Test_Data_ABS_White_1mm");

            await classInstance.EnsureEdgebandEntityExist("33", "Test_Data_ABS_White_1mm");
            await classInstance.EnsureEdgebandEntityExist("34", "Test_Data_ABS_White_1mm");
            await classInstance.EnsureEdgebandEntityExist("35", "Test_Data_ABS_White_1mm");
        }
           
        [TestMethod]
        public async Task EdgebandsGetEdgebandEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var result = await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntities(materialAssistClient);
            Assert.IsTrue(result.Count >= 3);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntityById()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var result2 = await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntityById(materialAssistClient);
            Assert.AreEqual("33", result2.Id);
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByIds()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var result3 = await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByIds(materialAssistClient);
            Assert.AreEqual(3, result3.Count());
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByEdgebandCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var result4 = await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCode(materialAssistClient);
            Assert.AreEqual(3, result4.Count());
        }

        [TestMethod]
        public async Task EdgebandsGetEdgebandEntitiesByEdgebandCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var result5 = await GetEndgebandEntitiesSamples.Edgebands_GetEdgebandEntitiesByEdgebandCodes(materialAssistClient);
            Assert.AreEqual(3, result5.Count());
        }

        [TestMethod]
        public async Task EdgebandsGetStorageLocations()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var result6 = await GetEndgebandEntitiesSamples.Edgebands_GetStorageLocations(materialAssistClient);
            Assert.IsTrue(result6.Count() >= 1);
        }

        [TestMethod]
        public async Task EdgebandsGetWorkstations()
        {
            var materialAssistClient = GetMaterialAssistClient().Edgebands;
            var result7 = await GetEndgebandEntitiesSamples.Edgebands_GetWorkstations(materialAssistClient);
            Assert.IsTrue(result7.Count() >= 1);
        }
    }
}
