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
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;
            try
            {
                var boardTypeRequest = new MaterialManagerRequestBoardType()
                {
                    BoardCode = "XTest_Data_EG_H3303_ST10_19_1000.0_500.0",
                    CoatingCategory = CoatingCategory.Undefined,
                    Grain = Grain.None,
                    Length = 1000.0,
                    Width = 500.0,
                    MaterialCategory = BoardMaterialCategory.Undefined,
                    MaterialCode = "Test_Data_EG_H3303_ST10_19",
                    Thickness = 19,
                    Type = BoardTypeType.Board,
                };
                await MaterialManagerClient.CreateBoardType(boardTypeRequest);
            }
            catch {}
        }

        [TestMethod]
        public async Task BoardsCreateOffcutEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await CreateOffcutEntitiesSamples.Boards_CreateOffcutEntity(MaterialAssistClient, "11114");
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await MaterialAssistClient.DeleteBoardEntity("11114");

            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;
            await MaterialManagerClient.DeleteBoardType("XTest_Data_EG_H3303_ST10_19_1000.0_500.0");
        }
    }
}
