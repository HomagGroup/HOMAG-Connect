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
            var boardCode = "XEG_H3303_ST10_19_1200.0_460.0";
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
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;

            await MaterialAssistClient.DeleteBoardEntity("22");
            try
            {
                await MaterialAssistClient.GetBoardEntityById("22");
                throw new Exception("Offcut entity was not deleted. Cleanup failed");
            }
            catch {/* Expected exception */}

            await MaterialManagerClient.DeleteBoardType("XEG_H3303_ST10_19_1200.0_460.0");
            try
            {
                await MaterialManagerClient.GetBoardTypeByBoardCode("XEG_H3303_ST10_19_1200.0_460.0");
                throw new Exception("Offcut type was not deleted. Cleanup failed");
            }
            catch {/* Expected exception */}
        }
    }
}
