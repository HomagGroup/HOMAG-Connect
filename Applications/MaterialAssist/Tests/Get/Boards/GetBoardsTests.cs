using HomagConnect.MaterialAssist.Samples.Get.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;

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

            await classInstance.EnsureBoardTypeExist("MDF_H3171_12_19.0");
            await classInstance.EnsureBoardTypeExist("EG_H3303_ST10_19");

            await classInstance.EnsureBoardEntityExist("733", "MDF_H3171_12_19.0_2800_2070");
            await classInstance.EnsureBoardEntityExist("734", "MDF_H3171_12_19.0_2800_2070", ManagementType.Stack, 5);
            await classInstance.EnsureBoardEntityExist("735", "EG_H3303_ST10_19_2800_2070", ManagementType.GoodsInStock, 5);
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
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntityById(materialAssistClient, "733");
            Assert.AreEqual("733", result.Id);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesById()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesById(materialAssistClient, ["733", "734", "735"]);
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(be => be.Id == "733"));
            Assert.IsTrue(result.Any(be => be.Id == "734"));
            Assert.IsTrue(result.Any(be => be.Id == "735"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(materialAssistClient, "MDF_H3171_12_19.0_2800_2070");
            Assert.IsTrue(result.Count() >= 2);
            Assert.IsTrue(result.Any(be => be.Id == "733"));
            Assert.IsTrue(result.Any(be => be.Id == "734"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(materialAssistClient, ["MDF_H3171_12_19.0_2800_2070", "EG_H3303_ST10_19_2800_2070"]);
            Assert.IsTrue(result.Count() >= 3);
            Assert.IsTrue(result.Any(be => be.Id == "733"));
            Assert.IsTrue(result.Any(be => be.Id == "734"));
            Assert.IsTrue(result.Any(be => be.Id == "735"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCode(materialAssistClient, "EG_H3303_ST10_19");
            Assert.IsTrue(result.Count() >= 1);
            Assert.IsTrue(result.Any(be => be.Id == "735"));
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            var result = await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(materialAssistClient, ["EG_H3303_ST10_19", "MDF_H3171_12_19.0"]);
            Assert.IsTrue(result.Count() >= 3);
            Assert.IsTrue(result.Any(be => be.Id == "733"));
            Assert.IsTrue(result.Any(be => be.Id == "734"));
            Assert.IsTrue(result.Any(be => be.Id == "735"));
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
