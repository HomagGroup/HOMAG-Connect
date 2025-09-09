using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Samples.Update.Boards;
using HomagConnect.MaterialAssist.Tests.Get.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Tests.Update.Boards
{ 
    [TestClass]
    [TestCategory("MaterialAssist")]
    [TestCategory("MaterialAssist.Boards")]

    public class UpdateBoardsTests : MaterialAssistTestBase
    {
        [ClassInitialize]
        public static async Task Initialize(TestContext testContext)
        {
            var classInstance = new UpdateBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;

            var boardTypeRequest = new MaterialManagerRequestBoardType()
            {
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                CoatingCategory = CoatingCategory.Undefined,
                Grain = Grain.None,
                Length = 2800.0,
                Width = 1310.0,
                MaterialCategory = BoardMaterialCategory.Undefined,
                MaterialCode = "MDF_H3171_12_11.69",
                Thickness = 11.6,
                Type = BoardTypeType.Board,
            };
            var newBoardType = await MaterialAssistClient.CreateBoardType(boardTypeRequest); 
            
            var boardTypeRequest2 = new MaterialManagerRequestBoardType()
            {
                BoardCode = "EG_H3303_ST10_19_2800.0_2070.0",
                CoatingCategory = CoatingCategory.MelamineThermoset,
                Grain = Grain.Lengthwise,
                Length = 2800.0,
                Width = 2070.0,
                MaterialCategory = BoardMaterialCategory.Chipboard,
                MaterialCode = "EG_H3303_ST10_19",
                Thickness = 19,
                Type = BoardTypeType.Board,
            };
            var newBoardType2 = await MaterialAssistClient.CreateBoardType(boardTypeRequest2);

            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Single,
                Quantity = 1
            };
            var newBoardEntitySingle = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = "50",
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Stack,
                Quantity = 5
            };
            var newBoardEntityStack = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = "28",
                BoardCode = "EG_H3303_ST10_19_2800.0_2070.0",
                ManagementType = ManagementType.GoodsInStock,
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await MaterialAssistClient.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        [TestMethod]
        public async Task BoardsRemoveAllBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveAllBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsRemoveSingleBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveSingleBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsRemoveSubsetBoardEntitiesFromWorkplace()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_RemoveSubsetBoardEntitiesFromWorkplace(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsStoreBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_StoreBoardEntity(MaterialAssistClient);
        }

        [TestMethod]
        public async Task BoardsUpdateBoardEntity()
        {
            var MaterialAssistClient = GetMaterialAssistClient().Boards;
            await UpdateBoardEntitiesSamples.Boards_UpdateBoardEntity(MaterialAssistClient);
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            var classInstance = new UpdateBoardsTests();
            var MaterialAssistClient = classInstance.GetMaterialAssistClient().Boards;
            var MaterialManagerClient = classInstance.GetMaterialManagerClient().Material.Boards;

            await MaterialAssistClient.DeleteBoardEntities(["42", "50", "28"]);
            await MaterialManagerClient.DeleteBoardType("EG_H3303_ST10_19_2800.0_2070.0");
            await MaterialManagerClient.DeleteBoardType("MDF_H3171_12_11.6_2800.0_1310.0");
        }
    }
}