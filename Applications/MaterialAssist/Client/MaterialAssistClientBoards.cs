using System.Text;

using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Boards;
using HomagConnect.MaterialAssist.Contracts.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Client
{
    /// <summary>
    /// materialAssist client for boards
    /// </summary>
    public class MaterialAssistClientBoards : ServiceBase, IMaterialAssistClientBoards
    {
        /// <inheritdoc />
        public MaterialAssistClientBoards(HttpClient client) : base(client) { }

        #region Delete

        /// <inheritdoc />
        public async Task DeleteBoardById(string id)
        {
            var url = $"{_BaseRoute}/delete?{_Id}={id}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        #endregion Delete

        #region Constants

        private const string _BaseRoute = "api/materialAssist/boards";
        private const string _BaseRouteMaterialManager = "api/materialManager/boards";
        private const string _Id = "Id";
        private const string _BoardCode = "boardCode";
        private const string _BoardTypeCode = "boardTypeCode";
        private const string _StorageLocation = "storageLocation";
        private const string _DeleteFromInventory = "deleteFromInventory";
        private const string _Length = "length";
        private const string _Width = "width";
        private const string _Comments = "comments";
        private const string _RemovalType = "removalType";

        #endregion Constants

        #region Read

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntities(int take, int skip = 0)
        {
            var url = $"{_BaseRoute}";

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<BoardEntity> GetBoardEntityById(string id)
        {
            var url = $"{_BaseRoute}?{_Id}={id}";

            return await RequestObject<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByIds(IEnumerable<string> ids)
        {
            var url = $"{_BaseRoute}";

            var boardCodes = new StringBuilder("?");
            boardCodes.Append(string.Join("&", ids.Select(id => $"{_Id}={id}")));

            url = url + boardCodes;

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCode(string boardCode)
        {
            var url = $"{_BaseRoute}?{_BoardCode}={boardCode}";

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCodes(IEnumerable<string> boardCodes)
        {
            var url = $"{_BaseRoute}";

            var appendedBoardCodes = new StringBuilder("?");
            appendedBoardCodes.Append(string.Join("&", boardCodes.Select(boardCode => $"{_BoardCode}={boardCode}")));

            url = url + boardCodes;

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StorageLocation>> GetStorageLocations()
        {
            var url = $"{_BaseRoute}/storageLocations";

            return await RequestEnumerable<StorageLocation>(new Uri(url, UriKind.Relative));
        }

        #endregion Read

        #region Create

        /// <inheritdoc />
        public async Task<BoardEntity> CreateBoardEntity(string boardTypeCode, BoardEntity boardEntity)
        {
            var url =
                $"{_BaseRoute}?{_BoardTypeCode}={boardTypeCode}";

            return await PostObject(new Uri(url, UriKind.Relative), boardEntity);
        }

        /// <inheritdoc />
        public async Task CreateBoardType(BoardType boardType)
        {
            var url = $"{_BaseRouteMaterialManager}/create";

            await PostObject(new Uri(url, UriKind.Relative), boardType);
        }

        #endregion Create

        #region Update

        /// <inheritdoc />
        public async Task UpdateBoardEntityDimensions(string id, double length, double width)
        {
            var url = $"{_BaseRoute}/update?{_Id}={id}&{_Length}={length}&{_Width}={width}";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task UpdateBoardEntityComment(string id, string comments)
        {
            var url = $"{_BaseRoute}/update?{_Id}={id}&{_Comments}={comments}";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task StoreBoardEntity(string id, int length, int width, StorageLocation storageLocation)
        {
            var url = $"{_BaseRoute}/store?{_Id}={id}&{_Length}={length}&{_Width}={width}&{_StorageLocation}={storageLocation}";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveAllBoardEntities(string id, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/remove?{_Id}={id}&{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=All";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveSubsetBoardEntities(string id, int quantity, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/remove?{_Id}={id}&{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=Subset";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveSingleBoardEntities(string id, int quantity, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/remove?{_Id}={id}&{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=Single";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        #endregion Update
    }
}