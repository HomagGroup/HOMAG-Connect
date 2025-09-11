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
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "13", "14", "15");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;

            await MaterialAssistClient.DeleteBoardEntity("13");
            await MaterialAssistClient.DeleteBoardEntity("14");
            await MaterialAssistClient.DeleteBoardEntity("15");
        }
    }
}
