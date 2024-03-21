using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    public interface IMaterialManagerClientMaterialBoards
    {
        Task<BoardType> GetBoardType(string boardCode);

        Task<IEnumerable<BoardType>> GetBoardTypes(int take, int skip = 0);

        Task<IEnumerable<BoardType>> GetBoardTypes(IEnumerable<string> boardCodes);

        Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCode(string materialCode);

        Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes);

        Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode);

        Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes);

        Task<IEnumerable<BoardTypeDetails>> GetBoardTypesIncludingDetails(IEnumerable<string> boardCodes);

        Task<IEnumerable<BoardCodeWithInventory>> GetBoardTypeInventory(IEnumerable<string> boardCodes);

        Task<IEnumerable<MaterialCodeWithThumbnail>> GetMaterialCodes(string search, int take, int skip = 0);

        Task<IEnumerable<MaterialCodeWithThumbnail>> GetMaterialCodes(int take, int skip = 0);
    }
}