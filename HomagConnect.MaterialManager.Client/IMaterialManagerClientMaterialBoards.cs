using System.Collections.Generic;
using System.Threading.Tasks;

using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialManager.Client
{
    public interface IMaterialManagerClientMaterialBoards
    {
        Task<BoardType> GetBoardType(string boardCode);

        Task<IReadOnlyCollection<BoardType>> GetBoardTypes(int take, int skip = 0);

        Task<IReadOnlyCollection<BoardType>> GetBoardTypes(IEnumerable<string> boardCodes);

        Task<IReadOnlyCollection<BoardType>> GetBoardTypesByMaterialCode(string materialCode);

        Task<IReadOnlyCollection<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes);

        Task<IReadOnlyCollection<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode);

        Task<IReadOnlyCollection<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes);

        Task<IReadOnlyCollection<BoardTypeDetails>> GetBoardTypesIncludingDetails(IEnumerable<string> boardCodes);

        Task<IReadOnlyCollection<BoardCodeWithInventory>> GetBoardTypeInventory(IEnumerable<string> boardCodes);

        Task<IReadOnlyCollection<MaterialCodeWithThumbnail>> GetMaterialCodes(string search, int take, int skip = 0);

        Task<IReadOnlyCollection<MaterialCodeWithThumbnail>> GetMaterialCodes(int take, int skip = 0);
    }
}