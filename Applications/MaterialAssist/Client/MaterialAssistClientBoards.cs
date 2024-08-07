﻿using System.Text;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Boards;
using HomagConnect.MaterialAssist.Contracts.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Request;

using Newtonsoft.Json;

namespace HomagConnect.MaterialAssist.Client
{
    /// <summary>
    /// materialAssist client for boards
    /// </summary>
    public class MaterialAssistClientBoards : ServiceBase, IMaterialAssistClientBoards
    {
        /// <inheritdoc />
        public MaterialAssistClientBoards(HttpClient client) : base(client) { }

        #region Private methods

        private static List<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "")
        {
            var urls = codes
                .Select(code => $"&{searchCode}={Uri.EscapeDataString(code)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.Remove(0, 1).Insert(0, "?"))
                .Select(parameter => $"{_BaseRoute}{route}" + parameter).ToList();

            return urls;
        }

        #endregion Private methods

        #region Delete

        /// <inheritdoc />
        public async Task DeleteBoardEntity(string id)
        {
            var url = $"{_BaseRoute}?{_Id}={Uri.EscapeDataString(id)}";

            await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteBoardEntities(IEnumerable<string> ids)
        {
            var url = $"{_BaseRoute}";

            var boardCodes = new StringBuilder("?");
            boardCodes.Append(string.Join("&", ids.Select(id => $"{_Id}={Uri.EscapeDataString(id)}")));

            url += boardCodes;

            await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
        }

        #endregion Delete

        #region Constants

        private const string _BaseRoute = "api/materialAssist/boardEntities";
        private const string _BaseRouteMaterialManager = "api/materialManager/boards";
        private const string _Id = "Id";
        private const string _BoardCode = "boardCode";
        private const string _StorageLocation = "storageLocation";
        private const string _DeleteFromInventory = "deleteFromInventory";
        private const string _Length = "length";
        private const string _Width = "width";
        private const string _Comments = "comments";
        private const string _RemovalType = "removalType";
        private const string _Quantity = "quantity";
        private const string _MaterialCode = "materialCode";

        #endregion Constants

        #region Read

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntities(int take, int skip = 0)
        {
            var url = $"{_BaseRoute}?take={take}&skip={skip}";

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<BoardEntity> GetBoardEntityById(string id)
        {
            var url = $"{_BaseRoute}?{_Id}={Uri.EscapeDataString(id)}";

            return await RequestObject<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByIds(IEnumerable<string> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var codes = ids
                .Where(b => !string.IsNullOrWhiteSpace(b))
                .Distinct()
                .OrderBy(b => b).ToList();

            if (!codes.Any())
            {
                throw new ArgumentNullException(nameof(ids), "At least one id must be passed.");
            }

            var urls = CreateUrls(codes, _Id);
            var boardEntities = new List<BoardEntity>();

            foreach (var url in urls)
            {
                boardEntities.AddRange(await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative)));
            }

            return boardEntities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCode(string boardCode)
        {
            var url = $"{_BaseRoute}?{_BoardCode}={Uri.EscapeDataString(boardCode)}";

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByBoardCodes(IEnumerable<string> boardCodes)
        {
            if (boardCodes == null)
            {
                throw new ArgumentNullException(nameof(boardCodes));
            }

            var codes = boardCodes
                .Where(b => !string.IsNullOrWhiteSpace(b))
                .Distinct()
                .OrderBy(b => b).ToList();

            if (!codes.Any())
            {
                throw new ArgumentNullException(nameof(boardCodes), "At least one board code must be passed.");
            }

            var urls = CreateUrls(codes, _BoardCode);
            var boardEntities = new List<BoardEntity>();

            foreach (var url in urls)
            {
                boardEntities.AddRange(await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative)));
            }

            return boardEntities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByMaterialCode(string materialCode)
        {
            var url = $"{_BaseRoute}?{_MaterialCode}={Uri.EscapeDataString(materialCode)}";

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<BoardEntity>> GetBoardEntitiesByMaterialCodes(IEnumerable<string> materialCodes)
        {
            if (materialCodes == null)
            {
                throw new ArgumentNullException(nameof(materialCodes));
            }

            var codes = materialCodes
                .Where(b => !string.IsNullOrWhiteSpace(b))
                .Distinct()
                .OrderBy(b => b).ToList();

            if (!codes.Any())
            {
                throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
            }

            var urls = CreateUrls(codes, _MaterialCode);
            var boardEntities = new List<BoardEntity>();

            foreach (var url in urls)
            {
                boardEntities.AddRange(await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative)));
            }

            return boardEntities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StorageLocation>> GetStorageLocations()
        {
            var url = $"{_BaseRoute}/storageLocations";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        public async Task<IEnumerable<StorageLocation>> GetStorageLocations(string workstation)
        {
            var url = $"{_BaseRoute}/storageLocations?workstation={workstation}";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        #endregion Read

        #region Create
        
        /// <inheritdoc />
        public async Task<BoardType> CreateBoardType(MaterialManagerRequestBoardType boardTypeRequest)
        {
            if (boardTypeRequest == null)
            {
                throw new ArgumentNullException(nameof(boardTypeRequest));
            }

            ValidateRequiredProperties(boardTypeRequest);

            var payload = JsonConvert.SerializeObject(boardTypeRequest);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await PostObject(new Uri(_BaseRouteMaterialManager, UriKind.Relative), content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BoardType>(responseContent);

            if (result != null)
            {
                return result;
            }

            throw new Exception($"The returned object is not of type {nameof(BoardType)}");
        }

        #endregion Create

        #region Update

        /// <inheritdoc />
        public async Task UpdateBoardEntityDimensions(string id, double length, double width)
        {
            var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}?{_Length}={length}&{_Width}={width}";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public async Task UpdateBoardEntityComments(string id, string comments)
        {
            var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}?{_Comments}={Uri.EscapeDataString(comments)}";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public async Task StoreBoardEntity(string id, int length, int width, StorageLocation storageLocation)
        {
            var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}/store?{_Length}={length}&{_Width}={width}&{_StorageLocation}={storageLocation}";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public async Task RemoveAllBoardEntitiesFromWorkplace(string id, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}/remove?{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=All";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public async Task RemoveSubsetBoardEntitiesFromWorkplace(string id, int quantity, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}/remove?{_Quantity}={quantity}{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=Subset";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public async Task RemoveSingleBoardEntitiesFromWorkplace(string id, int quantity, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRoute}/{Uri.EscapeDataString(id)}/remove?{_Quantity}={quantity}&{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=Single";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        #endregion Update
    }
}