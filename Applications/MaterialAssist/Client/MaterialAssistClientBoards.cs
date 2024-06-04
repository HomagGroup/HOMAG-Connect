using System.Data;
using System.Text;

using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Boards;
using HomagConnect.MaterialAssist.Contracts.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialAssist.Client
{
    public class MaterialAssistClientBoards : ServiceBase, IMaterialAssistClientBoards
    {
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
        private const string _IncludingDetails = "includingDetails";
        private const string _PrintLabel = "printLabel";
        private const string _StorageLocation = "storageLocation";
        private const string _DeleteBoardFromInventory = "deleteBoardFromInventory";
        private const string _Length = "length";
        private const string _Width = "width";
        private const string _Comments = "comments";
        private const string _ManagementType = "managementType";
        private const string _Quantity = "quantity";
        private const string _BoardTypeCode = "boardTypeCode";

        #endregion Constants

        #region Read

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntities()
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
        public async Task<BoardEntityDetails> GetBoardEntityByIdIncludingDetails(string id)
        {
            var url = $"{_BaseRoute}?{_Id}={id}&{_IncludingDetails}=true";

            return await RequestObject<BoardEntityDetails>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntityDetails>> GetBoardEntitiesByIdsIncludingDetails(IEnumerable<string> ids)
        {
            var url = $"{_BaseRoute}";

            var boardCodes = new StringBuilder("?");
            boardCodes.Append(string.Join("&", ids.Select(id => $"{_Id}={id}")));

            url = url + boardCodes + $"&{_IncludingDetails}=true";

            return await RequestEnumerable<BoardEntityDetails>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCode(string boardCode)
        {
            var url = $"{_BaseRoute}?{_BoardCode}={boardCode}";

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntityDetails>> GetBoardEntitiesByBoardCodeIncludingDetails(string boardCode)
        {
            var url = $"{_BaseRoute}?{_BoardCode}={boardCode}&{_IncludingDetails}=true";

            return await RequestEnumerable<BoardEntityDetails>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCodes(IEnumerable<string> boardCodes)
        {
            var url = $"{_BaseRoute}";

            var appendedBoardCodes = new StringBuilder("?");
            appendedBoardCodes.Append(string.Join("&", boardCodes.Select(boardCode => $"{_BoardCode}={boardCode}")));

            url = url + boardCodes;

            return await RequestEnumerable<BoardEntityDetails>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntityDetails>> GetBoardEntitiesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes)
        {
            var url = $"{_BaseRoute}";

            var appendedBoardCodes = new StringBuilder("?");
            appendedBoardCodes.Append(string.Join("&", boardCodes.Select(boardCode => $"{_BoardCode}={boardCode}")));

            url = url + boardCodes + $"&{_IncludingDetails}=true";

            return await RequestEnumerable<BoardEntityDetails>(new Uri(url, UriKind.Relative));
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
        public async Task CreateBoardEntity(string boardTypeCode, int quantity, ManagementType managementType, string comments,
            StorageLocation storageLocation, bool printLabel = false)
        {
            var url =
                $"{_BaseRoute}?{_BoardTypeCode}={boardTypeCode}&{_Quantity}={quantity}&{_ManagementType}={managementType}&{_Comments}={comments}&{_StorageLocation}={storageLocation}&{_PrintLabel}={printLabel}";

            await PostObject(new Uri(url, UriKind.Relative));
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

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task UpdateBoardEntityComment(string id, string comments)
        {
            var url = $"{_BaseRoute}/update?{_Id}={id}&{_Comments}={comments}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task PrintLabel(string id)
        {
            var url = $"{_BaseRoute}/print?{_Id}={id}&{_PrintLabel}=true";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task StoreBoardEntity(string id, int length, int width, StorageLocation storageLocation, bool printLabel = false)
        {
            var url = $"{_BaseRoute}/store?{_Id}={id}&{_StorageLocation}={storageLocation}&{_PrintLabel}=true";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveBoardEntity(string id, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/remove?{_Id}={id}&{_DeleteBoardFromInventory}={deleteBoardFromInventory}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        #endregion Update
    }
}