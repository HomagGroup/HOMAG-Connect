using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Get.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Get.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class GetBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new GetBoardsTests();

            await classInstance.EnsureBoardTypeExist("Test_Data_MDF_H3171_12_19.0");
            await classInstance.EnsureBoardTypeExist("Test_Data_EG_H3303_ST10_19");

            await classInstance.EnsureBoardEntityExist("31111", "Test_Data_MDF_H3171_12_19.0_2800_2070");
            await classInstance.EnsureBoardEntityExist("31112", "Test_Data_MDF_H3171_12_19.0_2800_2070", ManagementType.Stack, 5);
            await classInstance.EnsureBoardEntityExist("31113", "Test_Data_EG_H3303_ST10_19_2800_2070", ManagementType.GoodsInStock, 5);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntities(materialAssistClient);
            Assert.IsTrue(result.Count >= 3);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntityById()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntityById(materialAssistClient, "31111");
            Assert.AreEqual("31111", result.Id);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesById()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesById(materialAssistClient, ["31111", "31112", "31113"]);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(be => be.Id == "31111"));
            Assert.IsTrue(result.Any(be => be.Id == "31112"));
            Assert.IsTrue(result.Any(be => be.Id == "31113"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(materialAssistClient, "Test_Data_MDF_H3171_12_19.0_2800_2070");
            Assert.IsTrue(result.Count() >= 2);
            Assert.IsTrue(result.Any(be => be.Id == "31111"));
            Assert.IsTrue(result.Any(be => be.Id == "31112"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(materialAssistClient, ["Test_Data_MDF_H3171_12_19.0_2800_2070", "Test_Data_EG_H3303_ST10_19_2800_2070"]);
            Assert.IsTrue(result.Count() >= 3);
            Assert.IsTrue(result.Any(be => be.Id == "31111"));
            Assert.IsTrue(result.Any(be => be.Id == "31112"));
            Assert.IsTrue(result.Any(be => be.Id == "31113"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCode(materialAssistClient, "Test_Data_EG_H3303_ST10_19");
            Assert.IsTrue(result.Count() >= 1);
            Assert.IsTrue(result.Any(be => be.Id == "31113"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(materialAssistClient, ["Test_Data_EG_H3303_ST10_19", "Test_Data_MDF_H3171_12_19.0"]);
            Assert.IsTrue(result.Count() >= 3);
            Assert.IsTrue(result.Any(be => be.Id == "31111"));
            Assert.IsTrue(result.Any(be => be.Id == "31112"));
            Assert.IsTrue(result.Any(be => be.Id == "31113"));
        }

        [TestMethod]
        public async Task BoardsGetStorageLocations()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetStorageLocations(materialAssistClient);
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetStorageLocation()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetStorageLocation(materialAssistClient);
        }

        [TestMethod]
        public async Task BoardsGetWorkstations()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetWorkstations(materialAssistClient);
            // TODO: Add asserts
        }
    }
}
