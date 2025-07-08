using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Samples.Create.Boards
{
    public class CreateBoardEntitySample
    {
        public static async Task Boards_CreateBoardEntity(MaterialAssistClientBoards materialAssist)
        {
            var boardEntityRequest = new MaterialAssistRequestBoardEntity()
            {
                Id = "42",
                //The board code is the identifier of the board type
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                ManagementType = ManagementType.Single,
                Comments = "This is a comment",
                Quantity = 1
            };
            var newBoardEntity = await materialAssist.CreateBoardEntity(boardEntityRequest);
            Console.WriteLine($"Created board entity: {newBoardEntity.Id}");
        }

        public static async Task Boards_CreateBoardType(MaterialAssistClientBoards materialAssist)
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType()
            {
                BoardCode = "RP_EG_H3303_ST10_19",
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
