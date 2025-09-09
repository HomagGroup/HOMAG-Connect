using HomagConnect.MaterialAssist.Samples.Create.Offcuts;

namespace HomagConnect.MaterialAssist.Tests.Create.Offcuts
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateOffcutsTests : MaterialAssistTestBase
    {
        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardCode = "XEG_H3303_ST10_19_1000.0_500.0";
            var materialCode = "EG_H3303_ST10_19";
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutType(MaterialAssistClient, boardCode, materialCode);
        }

        [TestMethod]
        public async Task BoardsCreateOffcutEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(MaterialAssistClient, "22");
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new CreateOffcutsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;

            await MaterialAssistClient.DeleteBoardEntity("22");
            await MaterialManagerClient.DeleteBoardType("XEG_H3303_ST10_19_1000.0_500.0");
        }
    }
}
