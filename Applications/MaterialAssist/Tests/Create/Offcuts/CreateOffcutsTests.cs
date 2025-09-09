using HomagConnect.MaterialAssist.Samples.Create.Offcuts;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateOffcutsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task BoardsCreateOffcutEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(MaterialAssistClient, "14");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await MaterialAssistClient.DeleteBoardEntity("14");
        }
    }
}
