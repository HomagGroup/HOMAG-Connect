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
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "10", "11", "12");
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new CreateBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;

            await MaterialAssistClient.DeleteBoardEntity("10");
            await MaterialAssistClient.DeleteBoardEntity("11");
            await MaterialAssistClient.DeleteBoardEntity("12");
        }
    }
}
