using System.Text;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Boards;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Update;
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
        #region Private methods

        private static List<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "")
        {
            var urls = codes
                .Select(code => $"&{searchCode}={Uri.EscapeDataString(code)}")
                .Join(QueryParametersMaxLength)
                .Select(x => x.Remove(0, 1).Insert(0, "?"))
                .Select(parameter => $"{_BaseRouteMaterialAssist}{route}" + parameter).ToList();

            return urls;
        }

        #endregion Private methods

        #region Constructors

        /// <inheritdoc />
        public MaterialAssistClientBoards(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public MaterialAssistClientBoards(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public MaterialAssistClientBoards(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion

        #region Delete

        /// <inheritdoc />
        public async Task DeleteBoardEntity(string id)
        {
            var url = $"{_BaseRouteMaterialAssist}?{_Id}={Uri.EscapeDataString(id)}";

            await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteBoardEntities(IEnumerable<string> ids)
        {
            var url = $"{_BaseRouteMaterialAssist}";

            var boardCodes = new StringBuilder("?");
            boardCodes.Append(string.Join("&", ids.Select(id => $"{_Id}={Uri.EscapeDataString(id)}")));

            url += boardCodes;

            await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
        }

        #endregion Delete

        #region Constants

        private const string _BaseRouteMaterialAssist = "api/materialAssist/boardEntities";
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
            var url = $"{_BaseRouteMaterialAssist}?take={take}&skip={skip}";

            return await RequestEnumerable<BoardEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<BoardEntity> GetBoardEntityById(string id)
        {
            var url = $"{_BaseRouteMaterialAssist}?{_Id}={Uri.EscapeDataString(id)}";

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
            var url = $"{_BaseRouteMaterialAssist}?{_BoardCode}={Uri.EscapeDataString(boardCode)}";

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
            var url = $"{_BaseRouteMaterialAssist}?{_MaterialCode}={Uri.EscapeDataString(materialCode)}";

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
        public Task<IEnumerable<StorageLocation>> GetStorageLocations()
        {
            var url = $"{_BaseRouteMaterialAssist}/storageLocations";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        public Task<IEnumerable<StorageLocation>> GetStorageLocations(string workstation)
        {
            var url = $"{_BaseRouteMaterialAssist}/storageLocations?workstation={workstation}";
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
        
        public async Task<BoardEntity> CreateBoardEntity(MaterialAssistRequestBoardEntity boardEntityRequest)
        {
            {
                if (boardEntityRequest == null)
                {
                    throw new ArgumentNullException(nameof(boardEntityRequest));
                }

                ValidateRequiredProperties(boardEntityRequest);

                var payload = JsonConvert.SerializeObject(boardEntityRequest);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");
                var response = await PostObject(new Uri(_BaseRouteMaterialAssist, UriKind.Relative), content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BoardEntity>(responseContent);

                if (result != null)
                {
                    return result;
                }

                throw new Exception($"The returned object is not of type {nameof(BoardEntity)}");
            }
        }

        #endregion Create

        #region Update

        /// <inheritdoc />
        public async Task<BoardEntity> UpdateBoardEntity(string id, MaterialAssistUpdateBoardEntity updateBoardEntity)
        {
            if (updateBoardEntity == null)
            {
                throw new ArgumentNullException(nameof(updateBoardEntity));
            }

            ValidateRequiredProperties(updateBoardEntity);

            var url = $"{_BaseRouteMaterialAssist}?{_Id}={Uri.EscapeDataString(id)}";

            var payload = JsonConvert.SerializeObject(updateBoardEntity);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await PatchObject(new Uri(url, UriKind.Relative), content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BoardEntity>(responseContent);

            if (result != null)
            {
                return result;
            }

            throw new Exception($"The returned object is not of type {nameof(BoardEntity)}");
        }

        /// <inheritdoc />
        public Task StoreBoardEntity(string id, int length, int width, StorageLocation storageLocation)
        {
            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(id)}/store?{_Length}={length}&{_Width}={width}&{_StorageLocation}={storageLocation}";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public Task RemoveAllBoardEntitiesFromWorkplace(string id, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(id)}/remove?{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=All";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public Task RemoveSubsetBoardEntitiesFromWorkplace(string id, int quantity, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(id)}/remove?{_Quantity}={quantity}{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=Subset";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public Task RemoveSingleBoardEntitiesFromWorkplace(string id, int quantity, bool deleteBoardFromInventory = false)
        {
            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(id)}/remove?{_Quantity}={quantity}&{_DeleteFromInventory}={deleteBoardFromInventory}&{_RemovalType}=Single";
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        #endregion Update
    }
}