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

            IList<BoardEntity> boardEntities;

            do
            {
                boardEntities = await materialAssist.GetBoardEntities(take, skip).ToListAsync();

            } while (boardEntities.Count == take);

            foreach (var boardEntity in allBoardEntities)
            {
                Console.WriteLine($"Board entity ID: {boardEntity.Id}");
            }
        }

        // GetById
        public static async Task Boards_GetBoardEntityById(MaterialAssistClientBoards materialAssist, string id)
        {
            var boardEntity = await materialAssist.GetBoardEntityById(id);
            Console.WriteLine(boardEntity);
        }

        public static async Task Boards_GetBoardEntitiesById(MaterialAssistClientBoards materialAssist, List<string> ids)
        {
            var boardEntities = await materialAssist.GetBoardEntitiesByIds(ids);
            foreach (var boardEntity in boardEntities)
            {
                Console.WriteLine(boardEntity);
            }
        }

        // GetByBoardCode
        public static async Task Boards_GetBoardEntitiesByBoardCode(MaterialAssistClientBoards materialAssist, string boardCode)
        {
            var boardEntity = await materialAssist.GetBoardEntitiesByBoardCode(boardCode);
            Console.WriteLine(boardEntity);
        }

        public static async Task Boards_GetBoardEntitiesByBoardCodes(MaterialAssistClientBoards materialAssist, List<string> allBoardCodes)
        {
            var allBoardEntities = await materialAssist.GetBoardEntitiesByBoardCodes(allBoardCodes);
            foreach (var boardEntity in allBoardEntities)
            {
                Console.WriteLine(boardEntity);
            }
        }

        // GetByMaterialCode
        public static async Task Boards_GetBoardEntitiesByMaterialCode(MaterialAssistClientBoards materialAssist, string materialCode)
        {
            var boardEntity = await materialAssist.GetBoardEntitiesByMaterialCode(materialCode);
            Console.WriteLine(boardEntity);
        }

        public static async Task Boards_GetBoardEntitiesByMaterialCodes(MaterialAssistClientBoards materialAssist, List<string> allMaterialCodes)
        {
            var allBoardEntities = await materialAssist.GetBoardEntitiesByMaterialCodes(allMaterialCodes);
            foreach (var boardEntity in allBoardEntities)
            {
                Console.WriteLine(boardEntity);
            }
        }

        // GetStorageLocations
        public static async Task Boards_GetStorageLocations(MaterialAssistClientBoards materialAssist)
        {
            var allStorageLocations= await materialAssist.GetStorageLocations();
            var storageLocation = await materialAssist.GetStorageLocations("Compartment 02");

            Console.WriteLine(allStorageLocations);
            Console.WriteLine(storageLocation);
        }

        // GetWorkstations
        public static async Task Boards_GetWorkstations(MaterialAssistClientBoards materialAssist)
        {
            var workstations = await materialAssist.GetWorkstations();
            Console.WriteLine(workstations);
        }
    }
}
