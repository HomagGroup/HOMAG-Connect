using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Request;

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
    }
}
