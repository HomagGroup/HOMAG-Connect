using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Samples.Get.Boards
{
    public class GetBoardEntitiesSamples
    {
        public static async Task Boards_GetBoardEntities(MaterialAssistClientBoards materialAssist)
        {
            int take = 100000;
            int skip = 0;

            var allBoardEntities = new List<BoardEntity>();

            List<BoardEntity> boardEntities;

            do
            {
                boardEntities = (List<BoardEntity>)await materialAssist.GetBoardEntities(take, skip).ToListAsync();
                allBoardEntities.AddRange(boardEntities);
                skip += take;

            } while (boardEntities.Count == take);

            foreach (var boardEntity in allBoardEntities)
            {
                Console.WriteLine($"Board entity ID: {boardEntity.Id}");
            }
        }
    }
}
