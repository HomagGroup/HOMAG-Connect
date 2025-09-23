using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Samples.Create.Offcuts;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateOffcutsTests : MaterialAssistTestBase
    {
        [TestInitialize]
        public async Task Initialize()
        {
            // TODO: use valid names
            await EnsureBoardTypeExist("Test_Data_EG_H3303_ST10_19", 1000, 500, true);
        }

        [TestMethod]
        public async Task BoardsCreateOffcutEntity()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(materialAssistClient, "11114");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var materialAssistClient = GetMaterialAssistClient().Boards;
            await materialAssistClient.DeleteBoardEntity("11114");
        }
    }
}
