using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Delete.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class DeleteBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new DeleteBoardsTests();

            await classInstance.EnsureBoardTypeExists("Test_Data_MDF_H3171_12_19.0");
            await classInstance.EnsureBoardTypeExists("Test_Data_EG_H3303_ST10_19");

            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "21111",
                BoardCode = "Test_Data_MDF_H3171_12_19.0_2800_2070",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "21112",
                BoardCode = "Test_Data_MDF_H3171_12_19.0_2800_2070",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "21113",
                BoardCode = "Test_Data_EG_H3303_ST10_19_2800_2070",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntity(MaterialAssistClient, "21111");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(MaterialAssistClient, ["21112", "21113"]);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new DeleteBoardsTests();
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;
            await MaterialManagerClient.DeleteBoardType("Test_Data_MDF_H3171_12_19.0_2800_2070");
            await MaterialManagerClient.DeleteBoardType("Test_Data_EG_H3303_ST10_19_2800_2070");
        }
    }
}
