using HomagConnect.MaterialAssist.Samples.Create.Boards;

namespace HomagConnect.MaterialAssist.Tests.Create.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateBoardsTests : MaterialAssistTestBase
    {
        /*
        not possible to delete board type, because of no access to materialManager
        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardCode = "EG_H3303_ST10_19_2800.0_2070.0";
            await CreateBoardEntitySample.Boards_CreateBoardType(MaterialAssistClient, boardCode);
        }
        */

        [TestMethod]
        public async Task BoardsCreateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateBoardEntitySample.Boards_CreateBoardEntity(MaterialAssistClient, "42", "50", "23");
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new CreateBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;

            await MaterialAssistClient.DeleteBoardEntity("42");
            await MaterialAssistClient.DeleteBoardEntity("50");
            await MaterialAssistClient.DeleteBoardEntity("23");
        }
    }
}
