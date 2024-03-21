using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    public interface IMaterialManagerClientMaterialBoards
    {
        Task<BoardType> GetBoardType(string boardCode);

        IAsyncEnumerable<BoardType> GetBoardTypes(int take, int skip = 0);

        IAsyncEnumerable<BoardType> GetBoardTypes(IEnumerable<string> boardCodes);

        IAsyncEnumerable<BoardType> GetBoardTypesByMaterialCode(string materialCode);

        IAsyncEnumerable<BoardType> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes);

        IAsyncEnumerable<BoardTypeDetails> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode);

        IAsyncEnumerable<BoardTypeDetails> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes);

        IAsyncEnumerable<BoardTypeDetails> GetBoardTypesIncludingDetails(IEnumerable<string> boardCodes);

        IAsyncEnumerable<BoardCodeWithInventory> GetBoardTypeInventory(IEnumerable<string> boardCodes);

        IAsyncEnumerable<MaterialCodeWithThumbnail> GetMaterialCodes(string search, int take, int skip = 0);

        IAsyncEnumerable<MaterialCodeWithThumbnail> GetMaterialCodes(int take, int skip = 0);
    }
}