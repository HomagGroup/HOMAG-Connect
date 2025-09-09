using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using System.Threading.Tasks;

namespace HomagConnect.MaterialAssist.Tests.Create.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class CreateBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext context)
        {
            var classInstance = new CreateBoardsTests();
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;
                        
            var boardTypeRequest = new MaterialManagerRequestBoardType()
            {
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                Length = 2800.0,
                Width = 1310.0,
                Thickness = 11.6,
                Type = BoardTypeType.Board,
                MaterialCategory = BoardMaterialCategory.Undefined,
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None
            };
            await MaterialManagerClient.CreateBoardType(boardTypeRequest);
        }

        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var boardCode = "EG_H3303_ST10_19_2800.0_2070.0";
            await CreateBoardEntitySample.Boards_CreateBoardType(MaterialAssistClient, boardCode);
        }

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
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;

            await MaterialAssistClient.DeleteBoardEntities(["42", "50", "23"]);
            
            await MaterialManagerClient.DeleteBoardType("EG_H3303_ST10_19_2800.0_2070.0");
            await MaterialManagerClient.DeleteBoardType("MDF_H3171_12_11.6_2800.0_1310.0");
            
        }
    }
}
