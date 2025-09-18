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

            await classInstance.EnsureBoardTypeExists("Test_Data_MDF_H3171_12_19.0");
            await classInstance.EnsureBoardTypeExists("Test_Data_EG_H3303_ST10_19");

            // TODO: Ensure entity exists
            var materialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            try
            {
                var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
                {
                    Id = "31111",
                    BoardCode = "Test_Data_MDF_H3171_12_19.0_2800_2070",
                    ManagementType = ManagementType.Single,
                    Quantity = 1
                };
                var newBoardEntitySingle = await materialAssistClient.CreateBoardEntity(boardEntityRequestSingle);
            }
            catch { }
            try
            {
                var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
                {
                    Id = "31112",
                    BoardCode = "Test_Data_MDF_H3171_12_19.0_2800_2070",
                    ManagementType = ManagementType.Stack,
                    Quantity = 5
                };
                var newBoardEntityStack = await materialAssistClient.CreateBoardEntity(boardEntityRequestStack);
            }
            catch { }
            try
            {
                var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
                {
                    Id = "31113",
                    BoardCode = "Test_Data_EG_H3303_ST10_19_2800_2070",
                    ManagementType = ManagementType.GoodsInStock,
                    Quantity = 5
                };
                var newBoardEntityGoodsInStock = await materialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
            }
            catch { }
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntities(materialAssistClient);
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetBoardEntityById()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntityById(materialAssistClient, "31111");
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesById()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesById(materialAssistClient, ["31111", "31112", "31113"]);
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(materialAssistClient, "Test_Data_MDF_H3171_12_19.0_2800_2070");
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(materialAssistClient, ["Test_Data_MDF_H3171_12_19.0_2800_2070", "Test_Data_EG_H3303_ST10_19_2800_2070"]);
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCode()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCode(materialAssistClient, "Test_Data_EG_H3303_ST10_19");
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCodes()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(materialAssistClient, ["Test_Data_EG_H3303_ST10_19", "Test_Data_MDF_H3171_12_19.0"]);
            // TODO: Add asserts
        }

        [TestMethod]
        public async Task BoardsGetStorageLocations()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetStorageLocations(materialAssistClient);
            // TODO: Add asserts
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
