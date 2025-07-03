using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Delete.Boards
{
    public class DeleteBoardEntitiesSamples
    {
        public static async Task Boards_GetBoardEntities(MaterialAssistClientBoards materialAssist)
        {
            await materialAssist.DeleteBoardEntity("42");
        }
    }
}
