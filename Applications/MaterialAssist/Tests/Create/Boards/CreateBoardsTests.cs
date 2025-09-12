using HomagConnect.MaterialAssist.Samples.Create.Boards;

namespace HomagConnect.MaterialAssist.Tests.Create.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateBoardsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task BoardsCreateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "11111", "11112", "11113");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;

            await MaterialAssistClient.DeleteBoardEntity("11111");
            await MaterialAssistClient.DeleteBoardEntity("11112");
            await MaterialAssistClient.DeleteBoardEntity("11113");
        }
    }
}
