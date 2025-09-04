using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Samples.Create.Offcuts
{
    public class CreateOffcutEntitiesSamples
    {
        public static async Task Boards_CreateOffcutEntity(MaterialAssistClientBoards materialAssist, string id)
        {
            var boardEntityRequest = new MaterialAssistRequestOffcutEntity()
            {
                Id = id,
                //The board code is the identifier of the board type
                BoardCode = "XEG_H3303_ST10_19_1000.0_500.0",
                Comments = "This is a comment",
                Length = 1000,
                Width = 500,
                Quantity = 1,
            };
            var newBoardEntity = await materialAssist.CreateOffcutEntity(boardEntityRequest);
        }

        public static async Task Boards_CreateOffcutType(MaterialAssistClientBoards materialAssist, string boardCode, string materialCode)
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType()
            {
                BoardCode = boardCode,
                CoatingCategory = CoatingCategory.MelamineThermoset,
                Grain = Grain.Lengthwise,
                Length = 1200.0,
                Width = 460.0,
                MaterialCategory = BoardMaterialCategory.Chipboard,
                MaterialCode = materialCode,
                Thickness = 19,
                Type = BoardTypeType.Offcut,
            };
            var newBoardType = await materialAssist.CreateBoardType(boardTypeRequest);
        }
    }
}
