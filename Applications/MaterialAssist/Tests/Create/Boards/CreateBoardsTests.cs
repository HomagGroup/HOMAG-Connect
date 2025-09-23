using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialAssist.Tests.Create.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateBoardsTests : MaterialAssistTestBase
    {
        [TestInitialize]
        public async Task Initialize()
        {
            // TODO: use valid names
            await EnsureBoardTypeExist("Test_Data_MDF_H3171_12_19.0");
            await EnsureBoardTypeExist("Test_Data_EG_H3303_ST10_19");
        }

        [TestMethod]
        public async Task BoardsCreateBoardEntity()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(materialAssistClient, "11111", "11112", "11113");
            
            var boardEntity1 = await materialAssistClient.GetBoardEntityById("11111");
            Assert.AreEqual("11111", boardEntity1.Id);
            Assert.AreEqual(ManagementType.Single, boardEntity1.ManagementType);
            Assert.AreEqual(1, boardEntity1.Quantity);

            var boardEntity2 = await materialAssistClient.GetBoardEntityById("11112");
            Assert.AreEqual("11112", boardEntity2.Id);
            Assert.AreEqual(ManagementType.Stack, boardEntity2.ManagementType);
            Assert.AreEqual(5, boardEntity2.Quantity);

            var boardEntity3 = await materialAssistClient.GetBoardEntityById("11113");
            Assert.AreEqual("11113", boardEntity3.Id);
            Assert.AreEqual(ManagementType.GoodsInStock, boardEntity3.ManagementType);
            Assert.AreEqual(5, boardEntity3.Quantity);
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await materialAssistClient.DeleteBoardEntity("11111");
            await materialAssistClient.DeleteBoardEntity("11112");
            await materialAssistClient.DeleteBoardEntity("11113");
        }
    }
}
