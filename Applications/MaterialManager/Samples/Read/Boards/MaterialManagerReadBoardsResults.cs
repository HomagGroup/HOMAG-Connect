using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Samples.Helper;

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
            var boardTypeInventories = await materialManagerClientMaterialBoards.GetBoardTypesByBoardCodesIncludingDetails(boardCodes);
            Assert.IsNotNull(boardTypeInventories);
            boardTypeInventories.Select(m => m?.Inventory).Trace();
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

        /// <summary />
        public static async Task GetBoardTypesByExternalSystemId(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards,
            string externalSystemId)
        {
            var boardTypes = await materialManagerClientMaterialBoards.GetBoardTypesByExternalSystemId(externalSystemId, _Take);
            Assert.IsNotNull(boardTypes);
            boardTypes.Select(m => m.BoardCode).Trace();
        }

        /// <summary />
        public static async Task GetBoardTypesByExternalSystemIdIncludingDetails(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards,
            string externalSystemId)
        {
            var boardTypes = await materialManagerClientMaterialBoards.GetBoardTypesByExternalSystemId(externalSystemId, _Take, includingDetails: true);
            Assert.IsNotNull(boardTypes);
            boardTypes.Select(m => m.Inventory).Trace();
        }

        /// <summary />
        public static async Task GetBoardTypesFromCatalog(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)
        {
            // Browse the Tadamo catalog using paging (take / skip):
            var boardTypes = await materialManagerClientMaterialBoards.GetBoardTypesFromCatalog(_Take);
            Assert.IsNotNull(boardTypes);
            boardTypes.Select(m => m.BoardCode).Trace();
        }

        /// <summary />
        public static async Task GetBoardTypesFromCatalogFiltered(IMaterialManagerClientMaterialBoards materialManagerClientMaterialBoards)
        {
            // Filter the Tadamo catalog by manufacturer, decor code and embossing (all optional, exact match):
            var boardTypes = await materialManagerClientMaterialBoards.GetBoardTypesFromCatalog(
                _Take,
                skip: 0,
                manufacturers: ["Egger"],
                decorCode: "H3359",
                embossingTop: "32",
                embossingBottom: "32");
            Assert.IsNotNull(boardTypes);
            boardTypes.Select(m => m.DecorCode).Trace();
        }

    }
}