using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

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
            await EnsureBoardTypeExists("Test_Data_MDF_H3171_12_19.0");
            await EnsureBoardTypeExists("Test_Data_EG_H3303_ST10_19");
        }

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

            var MaterialMnagerClient = GetMaterialManagerClient().Material.Boards;
            await MaterialMnagerClient.DeleteBoardType("Test_Data_MDF_H3171_12_19.0_2800.0_2070.0");
            await MaterialMnagerClient.DeleteBoardType("Test_Data_EG_H3303_ST10_19_2800.0_2070.0");
        }
    }
}
