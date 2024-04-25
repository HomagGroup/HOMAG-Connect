using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Samples.Helper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HomagConnect.MaterialManager.Samples.Read.Boards
{
    /// <summary />
    public static class MaterialManagerReadBoardsResults
    {
        private const int _Take = 3;

        /// <summary />
        public static async Task GetLocations(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards,
            IEnumerable<string> boardCodes)
        {
            var boardTypeInventorys = await materialManagerClientMaterialBoards.GetBoardTypesByBoardCodesIncludingDetails(boardCodes);
            Assert.IsNotNull(boardTypeInventorys);
            boardTypeInventorys.Select(m => m.Inventory).Trace();
        }

        /// <summary />
        public static async Task GetMaterialCodes(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)
        {
            var materialCodes = await materialManagerClientMaterialBoards.GetBoardTypes(_Take);
            Assert.IsNotNull(materialCodes);
            materialCodes.Select(m => m.MaterialCode).Trace();
        }

        /// <summary />
        public static async Task GetThumbnails(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)
        {
            var materialCodes = await materialManagerClientMaterialBoards.GetBoardTypes(_Take);
            Assert.IsNotNull(materialCodes);
            materialCodes.Select(m => m.Thumbnail).Trace();
        }
    }
}