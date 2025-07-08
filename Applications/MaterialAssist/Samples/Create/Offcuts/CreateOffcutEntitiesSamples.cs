using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Samples.Create.Offcuts
{
    public class CreateOffcutEntitiesSamples
    {
        public static async Task Boards_CreateOffcutEntity(MaterialAssistClientBoards materialAssist)
        {
            var boardEntityRequest = new MaterialAssistRequestOffcutEntity()
            {
                Id = "42",
                //The board code is the identifier of the board type
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                Comments = "This is a comment",
                Length = 1000,
                Width = 50,
                Quantity = 5,
            };
            var newBoardEntity = await materialAssist.CreateOffcutEntity(boardEntityRequest);
            Console.WriteLine($"Created offcut entity: {newBoardEntity.Id}");
        }

        public static async Task Boards_CreateBoardType(MaterialAssistClientBoards materialAssist)
        {
            var boardTypeRequest = new MaterialManagerRequestBoardType()
            {
                BoardCode = "XEG_H3303_ST10_19_1200.0_460.0",
                CoatingCategory = CoatingCategory.MelamineThermoset,
                Grain = Grain.Lengthwise,
                Length = 1200.0,
                Width = 460.0,
                MaterialCategory = BoardMaterialCategory.Chipboard,
                MaterialCode = "EG_H3303_ST10_19",
                Thickness = 19,
                Type = BoardTypeType.Offcut,
            };
            var newBoardType = await materialAssist.CreateBoardType(boardTypeRequest);
        }
    }
}
