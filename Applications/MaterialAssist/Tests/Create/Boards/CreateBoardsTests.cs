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
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;
            try
            {
                var boardTypeRequest = new MaterialManagerRequestBoardType()
                {
                    BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                    CoatingCategory = CoatingCategory.Undefined,
                    Grain = Grain.None,
                    Length = 2800.0,
                    Width = 1310.0,
                    MaterialCategory = BoardMaterialCategory.Undefined,
                    MaterialCode = "Test_Data_MDF_H3171_12_11.6",
                    Thickness = 11.6,
                    Type = BoardTypeType.Board,
                };
                await MaterialManagerClient.CreateBoardType(boardTypeRequest);
            }
            catch { }
            try
            {
                var boardTypeRequest2 = new MaterialManagerRequestBoardType()
                {
                    BoardCode = "Test_Data_EG_H3303_ST10_19_2800.0_2070.0",
                    CoatingCategory = CoatingCategory.Undefined,
                    Grain = Grain.None,
                    Length = 2800.0,
                    Width = 2070.0,
                    MaterialCategory = BoardMaterialCategory.Undefined,
                    MaterialCode = "Test_Data_EG_H3303_ST10_19",
                    Thickness = 19,
                    Type = BoardTypeType.Board,
                };
                await MaterialManagerClient.CreateBoardType(boardTypeRequest2);
            }
            catch { }
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
            await MaterialMnagerClient.DeleteBoardType("Test_Data_MDF_H3171_12_11.6_2800.0_1310.0");
            await MaterialMnagerClient.DeleteBoardType("Test_Data_EG_H3303_ST10_19_2800.0_2070.0");
        }
    }
}
