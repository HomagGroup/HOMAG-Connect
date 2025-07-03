using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Material.Base;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialAssist.Samples.Create.Boards
{
    public class CreateBoardTypesSample
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
                // was kann man noch reinschreiben?
                BoardCode = "MDF_H3171_12_11.6_2800.0_1310.0",
                Comments = "This is a comment",
            };
            var newBoardEntity = await materialAssist.CreateBoardType(boardTypeRequest);
        }
    }
}
