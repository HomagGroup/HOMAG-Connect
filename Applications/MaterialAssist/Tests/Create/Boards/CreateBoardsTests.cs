using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Samples.Create.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
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
            var test = new CreateBoardsTests();
            var MaterialAssistClient = test.GetMaterialAssistClient().Boards;
            var MaterialManagerClient = test.GetMaterialManagerClient().Material.Boards;

            try
            {
                await MaterialManagerClient.GetBoardTypeByBoardCode("MDF_H3171_12_11.6_2800.0_1310.0");
            }
            catch 
            {                 
                var boardTypeRequest = new MaterialManager.Contracts.Request.MaterialManagerRequestBoardType()
                {
                    BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                    Length = 2800.0,
                    Width = 1310.0,
                    Thickness = 11.6,
                    Type = BoardTypeType.Board,
                    MaterialCategory = BoardMaterialCategory.Undefined,
                    CoatingCategory = CoatingCategory.MelamineThermoset,
                    Grain = Grain.None
                };
                await MaterialManagerClient.CreateBoardType(boardTypeRequest);
            }
        }

        [TestMethod]
        public async Task BoardsCreateBoardType()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            var MaterialManagerClient = GetMaterialManagerClient().Material.Boards;
            var boardCode = "EG_H3303_ST10_19_2800.0_2070.0";
            await CreateBoardEntitySample.Boards_CreateBoardType(MaterialAssistClient, boardCode);
            Assert.IsNotNull(await MaterialManagerClient.GetBoardTypeByBoardCode(boardCode));
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
            var test = new CreateBoardsTests();
            var MaterialAssistClient = test.GetMaterialAssistClient().Boards;
            var MaterialManagerClient = test.GetMaterialManagerClient().Material.Boards;

            await MaterialAssistClient.DeleteBoardEntities(["42", "50", "23"]);
            try
            {
                await MaterialAssistClient.GetBoardEntityById("42");
                throw new Exception("Board entity was not deleted. Cleanup failed");
            }
            catch {/* Expected exception */}

            await MaterialManagerClient.DeleteBoardType("RP_EG_H3303_ST10_19_2800.0_2070.0");
            try
            {
                await MaterialManagerClient.GetBoardTypeByBoardCode("RP_EG_H3303_ST10_19_2800.0_2070.0");
                throw new Exception("Board type was not deleted. Cleanup failed");
            }
            catch {/* Expected exception */}
        }
    }
}
