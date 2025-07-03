using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialAssist.Contracts.Update;

namespace HomagConnect.MaterialAssist.Samples.Update.Boards
{
    public class UpdateBoardEntitiesSamples
    {
        public static async Task Boards_UpdateBoardEntities(MaterialAssistClientBoards materialAssist)
        {
            var boardEntityUpdate = new MaterialAssistUpdateBoardEntity()
            {
                Id = "42",
                Length = 100,
                Comments = "This is a comment",
                Quantity = 1,
            };
            var updateBoardEntity = await materialAssist.UpdateBoardEntity("42", boardEntityUpdate);
            Console.WriteLine($"Updated board entity: {updateBoardEntity.Id}");
        }
    }
}
