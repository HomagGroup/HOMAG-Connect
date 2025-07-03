using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Samples.Get.Boards
{
    public class GetBoardEntitiesSamples
    {
        // GetAllEntities
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

        // GetById
        public static async Task Boards_GetBoardEntityById(MaterialAssistClientBoards materialAssist)
        {
            var boardEntity = await materialAssist.GetBoardEntityById("id");
            Console.WriteLine(boardEntity);
        }

        public static async Task Boards_GetBoardEntitiesById(MaterialAssistClientBoards materialAssist)
        {
            List<string> ids = ["id", "id", "id"];
            var boardEntities = await materialAssist.GetBoardEntitiesByIds(ids);
            foreach (var boardEntity in boardEntities)
            {
                Console.WriteLine(boardEntity);
            }
        }

        // GetByBoardCode
        public static async Task Boards_GetBoardEntitiesByBoardCode(MaterialAssistClientBoards materialAssist)
        {
            var boardEntity = await materialAssist.GetBoardEntitiesByBoardCode("boardCode");
            Console.WriteLine(boardEntity);
        }

        public static async Task Boards_GetBoardEntitiesByBoardCodes(MaterialAssistClientBoards materialAssist)
        {
            List<string> allBoardCodes = ["boardCode", "boardCode", "boardCode"];
            var allBoardEntities = await materialAssist.GetBoardEntitiesByBoardCodes(allBoardCodes);
            foreach (var boardEntity in allBoardEntities)
            {
                Console.WriteLine(boardEntity);
            }
        }

        // GetByMaterialCode
        public static async Task Boards_GetBoardEntitiesByMaterialCode(MaterialAssistClientBoards materialAssist)
        {
            var boardEntity = await materialAssist.GetBoardEntitiesByMaterialCode("materialCode");
            Console.WriteLine(boardEntity);
        }

        public static async Task Boards_GetBoardEntitiesByMaterialCodes(MaterialAssistClientBoards materialAssist)
        {
            List<string> allMaterialCodes = ["materialCode", "materialCode", "materialCode"];
            var allBoardEntities = await materialAssist.GetBoardEntitiesByMaterialCodes(allMaterialCodes);
            foreach (var boardEntity in allBoardEntities)
            {
                Console.WriteLine(boardEntity);
            }
        }

        // GetStorageLocations
        public static async Task Boards_GetStorageLocations(MaterialAssistClientBoards materialAssist)
        {
            var storageLocation1 = await materialAssist.GetStorageLocations();
            var storageLocation2 = await materialAssist.GetStorageLocations("workstationId");

            Console.WriteLine(storageLocation1);
            Console.WriteLine(storageLocation2);
        }

        // GetWorkstations
        public static async Task Boards_GetWorkstations(MaterialAssistClientBoards materialAssist)
        {
            var workstations = await materialAssist.GetWorkstations();
            Console.WriteLine(workstations);
        }
    }
}
