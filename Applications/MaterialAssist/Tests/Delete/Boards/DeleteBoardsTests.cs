using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;

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

            // TODO: Ensure entity exists
            var materialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "21111",
                BoardCode = "Test_Data_MDF_H3171_12_19.0_2800_2070",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "21112",
                BoardCode = "Test_Data_MDF_H3171_12_19.0_2800_2070",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "21113",
                BoardCode = "Test_Data_EG_H3303_ST10_19_2800_2070",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            await materialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        [TestMethod]
        public async Task BoardsDeleteBoardEntity()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntity(materialAssistClient, "21111");
        }

        [TestMethod]
        public async Task BoardsDeleteBoardEntities()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(materialAssistClient, ["21112", "21113"]);
        }
    }
}
