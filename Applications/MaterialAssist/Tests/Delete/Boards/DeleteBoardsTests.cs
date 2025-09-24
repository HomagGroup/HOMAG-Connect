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
            await classInstance.EnsureBoardTypeExist("MDF_H3171_12_19.0");
            await classInstance.EnsureBoardTypeExist("EG_H3303_ST10_19");

            await classInstance.EnsureBoardEntityExist("21111", "MDF_H3171_12_19.0_2800_2070");
            await classInstance.EnsureBoardEntityExist("21112", "MDF_H3171_12_19.0_2800_2070", ManagementType.Stack, 5);
            await classInstance.EnsureBoardEntityExist("21113", "EG_H3303_ST10_19_2800_2070", ManagementType.GoodsInStock, 5);
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
