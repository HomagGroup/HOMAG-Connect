using HomagConnect.MaterialAssist.Contracts.Request;
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
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;

            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "50",
                BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "23",
                BoardCode = "Test_Data_EG_H3303_ST10_19_2800.0_2070.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntities(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntityById()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntityById(MaterialAssistClient, "42");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesById()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesById(MaterialAssistClient, ["42", "50", "23"]);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCode()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(MaterialAssistClient, "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCodes()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(MaterialAssistClient, ["Test_Data_MDF_H3171_12_11.6_2800.0_1310.0", "Test_Data_EG_H3303_ST10_19_2800.0_2070.0"]);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCode()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCode(MaterialAssistClient, "Test_Data_EG_H3303_ST10_19");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByMaterialCodes()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(MaterialAssistClient, ["Test_Data_EG_H3303_ST10_19", "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0"]);
        }

        [TestMethod]
        public async Task BoardsGetStorageLocations()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetStorageLocations(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsGetWorkstations()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetWorkstations(MaterialAssistClient);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new GetBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            await MaterialAssistClient.DeleteBoardEntities(["42", "50", "23"]);
        }
    }
}
