using System.Text;

using HomagConnect.Base.Services;
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
        public async Task DeleteBoardByCode(int code)
        {
            var url = $"{_BaseRoute}/delete?{_Code}={code}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        #endregion Delete

        #region Constants

        private const string _BaseRoute = "api/materialAssist/boards";
        private const string _BaseRouteMaterialManager = "api/materialManager/boards";
        private const string _Code = "Code";
        private const string _IncludingDetails = "includingDetails";
        private const string _FromStorage = "fromStorage";
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
        public async Task<IEnumerable<Board>> GetBoardsFromInventory()
        {
            var url = $"{_BaseRoute}?{_FromStorage}=false";

            return await RequestEnumerable<Board>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Board>> GetBoardsFromStorage()
        {
            var url = $"{_BaseRoute}?{_FromStorage}=true";

            return await RequestEnumerable<Board>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<Board> GetBoardByCode(int code)
        {
            var url = $"{_BaseRoute}?{_Code}={code}";

            return await RequestObject<Board>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Board>> GetBoardsByCodes(IEnumerable<int> codes)
        {
            var url = $"{_BaseRoute}";

            var boardCodes = new StringBuilder("?");
            boardCodes.Append(string.Join("&", codes.Select(bc => $"{_Code}={bc}")));

            url = url + boardCodes;

            return await RequestEnumerable<Board>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<BoardDetails> GetBoardByCodeIncludingDetails(int code)
        {
            var url = $"{_BaseRoute}?{_Code}={code}&{_IncludingDetails}=true";

            return await RequestObject<BoardDetails>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardDetails>> GetBoardsByCodesIncludingDetails(IEnumerable<int> codes)
        {
            var url = $"{_BaseRoute}";

            var boardCodes = new StringBuilder("?");
            boardCodes.Append(string.Join("&", codes.Select(bc => $"{_Code}={bc}")));

            url = url + boardCodes + $"&{_IncludingDetails}=true";

            return await RequestEnumerable<BoardDetails>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetStorageLocations()
        {
            var url = $"{_BaseRoute}/storageLocations";

            return await RequestEnumerable<string>(new Uri(url, UriKind.Relative));
        }

        #endregion Read

        #region Create

        /// <inheritdoc />
        public async Task CreateBoard(string boardTypeCode, int quantity, ManagementType managementType, string comments,
            string storageLocation, bool printLabel = false)
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
        public async Task UpdateBoardByCode(int code, double length, double width, string comments)
        {
            var url = $"{_BaseRoute}/update?{_Code}={code}&{_Length}={length}&{_Width}={width}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task PrintLabelByCode(int code)
        {
            var url = $"{_BaseRoute}/print?{_Code}={code}&{_PrintLabel}=true";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task StoreBoardByCode(int code, int length, int width, string storageLocation, bool printLabel = false)
        {
            var url = $"{_BaseRoute}/store?{_Code}={code}&{_StorageLocation}={storageLocation}&{_PrintLabel}=true";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveBoardByCode(int code, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/remove?{_Code}={code}&{_DeleteBoardFromInventory}={deleteBoardFromInventory}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        #endregion Update
    }
}