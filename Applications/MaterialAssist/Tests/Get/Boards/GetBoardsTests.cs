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

            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            try
            {
                var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
                {
                    Id = "31111",
                    BoardCode = "Test_Data_MDF_H3171_12_19.0_2800.0_2070.0",
                    ManagementType = ManagementType.Single,
                    Quantity = 1
                };
                var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);
            }
            catch { }
            try
            {
                var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
                {
                    Id = "31112",
                    BoardCode = "Test_Data_MDF_H3171_12_19.0_2800.0_2070.0",
                    ManagementType = ManagementType.Stack,
                    Quantity = 5
                };
                var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);
            }
            catch { }
            try
            {
                var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
                {
                    Id = "31113",
                    BoardCode = "Test_Data_EG_H3303_ST10_19_2800.0_2070.0",
                    ManagementType = ManagementType.GoodsInStock,
                    Quantity = 5
                };
                var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
            }
            catch { }
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
            await GetBoardEntitiesSamples.Boards_GetBoardEntityById(MaterialAssistClient, "31111");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesById()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesById(MaterialAssistClient, ["31111", "31112", "31113"]);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCode()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCode(MaterialAssistClient, "Test_Data_MDF_H3171_12_19.0_2800.0_2070.0");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntitiesByBoardCodes()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByBoardCodes(MaterialAssistClient, ["Test_Data_MDF_H3171_12_19.0_2800.0_2070.0", "Test_Data_EG_H3303_ST10_19_2800.0_2070.0"]);
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
            await GetBoardEntitiesSamples.Boards_GetBoardEntitiesByMaterialCodes(MaterialAssistClient, ["Test_Data_EG_H3303_ST10_19", "Test_Data_MDF_H3171_12_19.0"]);
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
            await MaterialAssistClient.DeleteBoardEntity("31111");
            await MaterialAssistClient.DeleteBoardEntity("31112");
            await MaterialAssistClient.DeleteBoardEntity("31113");

            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;
            await MaterialManagerClient.DeleteBoardType("Test_Data_MDF_H3171_12_19.0_2800.0_2070.0");
            await MaterialManagerClient.DeleteBoardType("Test_Data_EG_H3303_ST10_19_2800.0_2070.0");
        }
    }
}
