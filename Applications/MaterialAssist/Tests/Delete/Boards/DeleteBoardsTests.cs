using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Delete.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Delete.Boards
{
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]
    public class DeleteBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new DeleteBoardsTests();

            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;
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

            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "21111",
                BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "21112",
                BoardCode = "Test_Data_MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "21113",
                BoardCode = "Test_Data_EG_H3303_ST10_19_2800.0_2070.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        [TestMethod]
        public async Task BoardsGetBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntity(MaterialAssistClient, "21111");
        }

        [TestMethod]
        public async Task BoardsGetBoardEntities()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await DeleteBoardEntitiesSamples.Boards_DeleteBoardEntities(MaterialAssistClient, ["21112", "21113"]);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new DeleteBoardsTests();
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;
            await MaterialManagerClient.DeleteBoardType("Test_Data_MDF_H3171_12_11.6_2800.0_1310.0");
            await MaterialManagerClient.DeleteBoardType("Test_Data_EG_H3303_ST10_19_2800.0_2070.0");
        }
    }
}
