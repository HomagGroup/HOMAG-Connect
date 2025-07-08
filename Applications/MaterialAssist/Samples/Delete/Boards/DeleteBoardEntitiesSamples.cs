using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Delete.Boards
{
    public class DeleteBoardEntitiesSamples
    {
        public static async Task Boards_GetBoardEntity(MaterialAssistClientBoards materialAssist)
        {
            await materialAssist.DeleteBoardEntity("42");
        }

        public static async Task Boards_GetBoardEntities(MaterialAssistClientBoards materialAssist)
        {
            List<string> ids = ["42", "50", "23"];
            await materialAssist.DeleteBoardEntities(ids);
        }
    }
}
