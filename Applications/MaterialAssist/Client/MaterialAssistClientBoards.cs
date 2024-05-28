using System.Data;
using System.Text;

using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;
using HomagConnect.MaterialAssist.Contracts.Boards;

namespace HomagConnect.MaterialAssist.Client
{
    public class MaterialAssistClientBoards : ServiceBase
    {
        public MaterialAssistClientBoards(HttpClient client) : base(client) { }

        #region Constants

        private const string _BaseRoute = "api/materialAssist/boards";
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
        public async Task<IEnumerable<Board>> GetBoardsFromInventory()
        {
            var url = $"{_BaseRoute}?{_FromStorage}=false";

            return await RequestEnumerable<Board>(new Uri(url, UriKind.Relative));
        }

        public async Task<IEnumerable<Board>> GetBoardsFromStorage()
        {
            var url = $"{_BaseRoute}?{_FromStorage}=true";

            return await RequestEnumerable<Board>(new Uri(url, UriKind.Relative));
        }

        public async Task<Board> GetBoardByBoardCode(string code)
        {
            var url = $"{_BaseRoute}?{_Code}={code}";

            return await RequestObject<Board>(new Uri(url, UriKind.Relative));
        }

        public async Task<IEnumerable<Board>> GetBoardsByBoardCode(IEnumerable<string> code)
        {
            var url = $"{_BaseRoute}";

            var boardCodes = new StringBuilder("?");
            boardCodes.AppendJoin("&", code.Select(c => $"{_Code}={c}"));

            url = url + boardCodes;

            return await RequestEnumerable<Board>(new Uri(url, UriKind.Relative));
        }

        public async Task<BoardDetails> GetBoardByBoardCodeIncludingDetails(string code)
        {
            var url = $"{_BaseRoute}?{_Code}={code}&{_IncludingDetails}=true";

            return await RequestObject<BoardDetails>(new Uri(url, UriKind.Relative));
        }

        public async Task<IEnumerable<BoardDetails>> GetBoardsByBoardCodeIncludingDetails(IEnumerable<string> code)
        {
            var url = $"{_BaseRoute}";

            var boardCodes = new StringBuilder("?");
            boardCodes.Append(string.Join("&", code.Select(bc => $"{_Code}={bc}")));
            
            url = url + boardCodes + $"&{_IncludingDetails}=true";

            return await RequestEnumerable<BoardDetails>(new Uri(url, UriKind.Relative));
        }

        #endregion Read

        #region Create

        public async Task CreateBoard(string boardTypeCode, int quantity, ManagementType managementType, string comments, string storageLocation, bool printLabel = false)
        {
            var url = $"{_BaseRoute}?{_BoardTypeCode}={boardTypeCode}&{_Quantity}={quantity}&{_ManagementType}={managementType}&{_Comments}={comments}&{_StorageLocation}={storageLocation}&{_PrintLabel}={printLabel}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        #endregion Create

        #region Update

        public async Task UpdateBoard(int code, double length, double width, string comments)
        {
            var url = $"{_BaseRoute}/update?{_Code}={code}&{_Length}={length}&{_Width}={width}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        public async Task PrintLabel(int code)
        {
            var url = $"{_BaseRoute}/print?{_Code}={code}&{_PrintLabel}=true";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        public async Task StoreBoard(int code, int length, int width, string storageLocation, bool printLabel = false)
        {
            var url = $"{_BaseRoute}/store?{_Code}={code}&{_StorageLocation}={storageLocation}&{_PrintLabel}=true";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        public async Task RemoveBoard(int code, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/remove?{_Code}={code}&{_DeleteBoardFromInventory}={deleteBoardFromInventory}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        #endregion Update

        #region Delete

        public async Task DeleteBoard(int code)
        {
            var url = $"{_BaseRoute}/delete?{_Code}={code}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        #endregion Delete
    }
}