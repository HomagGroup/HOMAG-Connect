using HomagConnect.MaterialAssist.Client;

namespace HomagConnect.MaterialAssist.Samples.Delete.Boards
{
    public class DeleteBoardEntitiesSamples
    {
        public static async Task Boards_GetBoardEntity(MaterialAssistClientBoards materialAssist)
        {
            await materialAssist.DeleteBoardEntity("id");
        }

        public static async Task Boards_GetBoardEntities(MaterialAssistClientBoards materialAssist)
        {
            List<string> ids = ["id", "id", "id"];
            await materialAssist.DeleteBoardEntities(ids);
        }
    }
}
