using HomagConnect.Base.Extensions;
using HomagConnect.MaterialAssist.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Samples.Get.Boards
{
    public class GetBoardEntitiesSamples
    {
        // GetAllEntities
        public static async Task<IList<BoardEntity>> Boards_GetBoardEntities(MaterialAssistClientBoards materialAssist)
        {
            var result = await materialAssist.GetBoardEntities(100000, 0);
            return result != null? result.ToList() : [];
        }

        // GetById
        public static async Task<BoardEntity?> Boards_GetBoardEntityByCode(MaterialAssistClientBoards materialAssist, string code)
        {
            var boardEntity = await materialAssist.GetBoardEntityByCode(code);
            return boardEntity;
        }

        public static async Task<IEnumerable<BoardEntity>> Boards_GetBoardEntitiesByCodes(MaterialAssistClientBoards materialAssist, List<string> codes)
        {
            var boardEntities = await materialAssist.GetBoardEntitiesByCodes(codes);
            return boardEntities;
        }

        // GetByBoardCode
        public static async Task<IEnumerable<BoardEntity>?> Boards_GetBoardEntitiesByBoardCode(MaterialAssistClientBoards materialAssist, string boardCode)
        {
            var boardEntity = await materialAssist.GetBoardEntitiesByBoardCode(boardCode);
            return boardEntity;
        }

        public static async Task<IEnumerable<BoardEntity>> Boards_GetBoardEntitiesByBoardCodes(MaterialAssistClientBoards materialAssist, List<string> allBoardCodes)
        {
            var allBoardEntities = await materialAssist.GetBoardEntitiesByBoardCodes(allBoardCodes);
            return allBoardEntities;
        }

        // GetByMaterialCode
        public static async Task<IEnumerable<BoardEntity>?> Boards_GetBoardEntitiesByMaterialCode(MaterialAssistClientBoards materialAssist, string materialCode)
        {
            var boardEntity = await materialAssist.GetBoardEntitiesByMaterialCode(materialCode);
            return boardEntity;
        }

        public static async Task<IEnumerable<BoardEntity>> Boards_GetBoardEntitiesByMaterialCodes(MaterialAssistClientBoards materialAssist, List<string> allMaterialCodes)
        {
            var allBoardEntities = await materialAssist.GetBoardEntitiesByMaterialCodes(allMaterialCodes);
            return allBoardEntities;
        }

        // GetStorageLocations
        public static async Task<IEnumerable<Base.Contracts.StorageLocation>> Boards_GetStorageLocations(MaterialAssistClientBoards materialAssist)
        {
            var allStorageLocations= await materialAssist.GetStorageLocations();
            return allStorageLocations;
        }

        public static async Task<IEnumerable<Base.Contracts.StorageLocation>> Boards_GetStorageLocation(MaterialAssistClientBoards materialAssist)
        {
            var storageLocation = await materialAssist.GetStorageLocations("Compartment 02");
            return storageLocation;
        }

        // GetWorkstations
        public static async Task<IEnumerable<Base.Contracts.Workstation>> Boards_GetWorkstations(MaterialAssistClientBoards materialAssist)
        {
            var workstations = await materialAssist.GetWorkstations();
            return workstations;
        }
    }
}
