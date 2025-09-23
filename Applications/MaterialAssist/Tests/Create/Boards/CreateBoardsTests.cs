using HomagConnect.MaterialAssist.Samples.Create.Boards;

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
