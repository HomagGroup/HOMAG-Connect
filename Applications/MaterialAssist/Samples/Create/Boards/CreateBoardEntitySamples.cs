using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using System;

namespace HomagConnect.MaterialAssist.Samples.Create.Boards
{
    public class CreateBoardEntitySample
    {
        public static async Task Boards_CreateBoardEntity(MaterialAssistClientBoards materialAssist, string idSingle, string idStack, string idGoodsInStock)
        {
            var boardEntityRequestSingle = new MaterialAssistRequestBoardEntity()
            {
                Id = idSingle,
                //The board code is the identifier of the board type
                BoardCode = "MDF_H3171_12_19.0_2800_2070",
                ManagementType = ManagementType.Single,
                Comments = "This is a comment",
                Quantity = 1
            };
            var newBoardEntitySingle = await materialAssist.CreateBoardEntity(boardEntityRequestSingle);

            var boardEntityRequestStack = new MaterialAssistRequestBoardEntity()
            {
                Id = idStack,
                //The board code is the identifier of the board type
                BoardCode = "MDF_H3171_12_19.0_2800_2070",
                ManagementType = ManagementType.Stack,
                Comments = "This is a comment",
                Quantity = 5
            };
            var newBoardEntityStack = await materialAssist.CreateBoardEntity(boardEntityRequestStack);

            var boardEntityRequestGoodsInStock = new MaterialAssistRequestBoardEntity()
            {
                Id = idGoodsInStock,
                //The board code is the identifier of the board type
                BoardCode = "EG_H3303_ST10_19_2800_2070",
                ManagementType = ManagementType.GoodsInStock,
                Comments = "This is a comment",
                Quantity = 5
            };
            var newBoardEntityGoodsInStock = await materialAssist.CreateBoardEntity(boardEntityRequestGoodsInStock);
        }

        public static async Task Boards_CreateBoardType(MaterialAssistClientBoards materialAssist, string boardCode)
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType()
            {
                BoardCode = boardCode,
                CoatingCategory = CoatingCategory.MelamineThermoset,
                Grain = Grain.Lengthwise, 
                Length = 2800.0,
                Width = 2070.0,
                MaterialCategory = BoardMaterialCategory.Chipboard,
                MaterialCode = "EG_H3303_ST10_19",
                Thickness = 19,
                Type = BoardTypeType.Board,
            };
            var newBoardType = await materialAssist.CreateBoardType(boardTypeRequest);
        }
    }
}
