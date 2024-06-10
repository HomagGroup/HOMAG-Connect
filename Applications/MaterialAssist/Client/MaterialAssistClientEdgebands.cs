using System.Text;

using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Edgebands;
using HomagConnect.MaterialAssist.Contracts.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialAssist.Client
{
    /// <summary>
    /// materialAssist client for edgebands
    /// </summary>
    public class MaterialAssistClientEdgebands : ServiceBase, IMaterialAssistClientEdgebands
    {
        /// <inheritdoc />
        public MaterialAssistClientEdgebands(HttpClient client) : base(client) { }

        #region Delete

        /// <inheritdoc />
        public async Task DeleteEdgebandEntity(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRoute}?/delete?{_Id}={Uri.EscapeDataString(id)}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DeleteEdgebandEntity(IEnumerable<string> ids)
        {
            var url = $"{_BaseRoute}";

            var edgebandCodes = new StringBuilder("?");
            edgebandCodes.Append(string.Join("&", ids.Select(id => $"{_Id}={id}")));

            url = url + edgebandCodes;

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        #endregion Delete

        #region Private methods

        private static List<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "",
            bool includingDetails = false)
        {
            var queryParameters = new StringBuilder("?");
            var i = 1;
            var urls = new List<string>();
            var codeList = codes.ToList();
            while (i <= codeList.Count)
            {
                queryParameters.Append($"{searchCode}={Uri.EscapeDataString(codeList[i - 1])}");
                // To reduce the size of the URL, we are going to split the request into multiple requests. Max URL length is 2048, that´s why we are using 1900 as the limit with a little bit of added buffer.
                if (queryParameters.Length + _BaseRoute.Length > QueryParametersMaxLength)
                {
                    urls.Add(includingDetails
                        ? $"{_BaseRoute}{route}{queryParameters}&{_IncludingDetails}=true"
                        : $"{_BaseRoute}{route}{queryParameters}");

                    queryParameters = new StringBuilder("?");
                }

                i++;
                if (i <= codeList.Count)
                {
                    queryParameters.Append('&');
                }
            }

            urls.Add(includingDetails
                ? $"{_BaseRoute}{route}{queryParameters}&{_IncludingDetails}=true"
                : $"{_BaseRoute}{route}{queryParameters}");

            return urls;
        }

        #endregion Private methods

        #region Constants

        private const string _BaseRoute = "api/materialAssist/edgebands";
        private const string _BaseRouteMaterialManager = "api/materialManager/edgebands";
        private const string _Id = "Id";
        private const string _EdgebandCode = "edgebandCode";
        private const string _EdgebandTypeCode = "edgebandTypeCode";
        private const string _StorageLocation = "storageLocation";
        private const string _DeleteFromInventory = "deleteFromInventory";
        private const string _Length = "length";
        private const string _CurrentThickness = "currentThickness";
        private const string _Comments = "comments";
        private const string _RemovalType = "removalType";
        private const string _IncludingDetails = "includingDetails";

        #endregion Constants

        #region Read

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>> GetEdgebandEntities(int take, int skip = 0)
        {
            var url = $"{_BaseRoute}?take={take}&skip={skip}";

            return await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<EdgebandEntity> GetEdgebandEntityById(string id)
        {
            var url = $"{_BaseRoute}?{_Id}={Uri.EscapeDataString(id)}";

            return await RequestObject<EdgebandEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCode(string edgebandCode)
        {
            var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}";

            return await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCodes(IEnumerable<string> edgebandCodes)
        {
            if (edgebandCodes == null)
            {
                throw new ArgumentNullException(nameof(edgebandCodes));
            }

            var codes = edgebandCodes
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            if (!codes.Any())
            {
                throw new ArgumentNullException(nameof(edgebandCodes), "At least one material code must be passed.");
            }

            var urls = CreateUrls(codes, _EdgebandCode);
            var edgebandEntities = new List<EdgebandEntity>();

            foreach (var url in urls)
            {
                edgebandEntities.AddRange(await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative)));
            }

            return edgebandEntities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByIds(IEnumerable<string> ids)
        {
            if (ids == null)
            {
                throw new ArgumentNullException(nameof(ids));
            }

            var codes = ids
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .OrderBy(m => m)
                .ToList();

            if (!codes.Any())
            {
                throw new ArgumentNullException(nameof(ids), "At least one material code must be passed.");
            }

            var urls = CreateUrls(codes, _Id);
            var boardTypes = new List<EdgebandEntity>();

            foreach (var url in urls)
            {
                boardTypes.AddRange(await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative)));
            }

            return boardTypes;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StorageLocation>> GetStorageLocations()
        {
            var url = $"{_BaseRoute}/storageLocations";

            return await RequestEnumerable<StorageLocation>(new Uri(url, UriKind.Relative));
        }

        #endregion Read

        #region Update

        /// <inheritdoc />
        public async Task UpdateEdgebandEntityDimensions(string id, double length, double currentThickness)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            if (length <= 0.1)
            {
                throw new ArgumentException("Length must be higher than 0.1", nameof(length));
            }

            if (currentThickness <= 0.01)
            {
                throw new ArgumentException("Current thickness must be higher than 0.01", nameof(currentThickness));
            }

            var url =
                $"{_BaseRoute}/update?{_Id}={Uri.EscapeDataString(id)}&{_Length}={length}&{_CurrentThickness}={currentThickness}";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task UpdateEdgebandEntityComments(string id, string comments)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url =
                $"{_BaseRoute}/update?{_Id}={Uri.EscapeDataString(id)}&{_Comments}={comments}";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        public async Task StoreEdgebandEntity(string id, string storageLocation, double length)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            if (string.IsNullOrEmpty(storageLocation))
            {
                throw new ArgumentException("Storage location must be higher than 0", nameof(storageLocation));
            }

            if (length <= 0.1)
            {
                throw new ArgumentException("Length must be higher than 0.1", nameof(length));
            }

            var url =
                $"{_BaseRoute}/store?{_Id}={Uri.EscapeDataString(id)}&{_StorageLocation}={storageLocation}&{_Length}={length}";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveAllEdgebandEntities(string id, bool deleteFromInventory = false)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRoute}/remove?{_Id}={Uri.EscapeDataString(id)}&{_DeleteFromInventory}={deleteFromInventory}&{_RemovalType}=All";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveSingleEdgebandEntities(string id, int quantity, bool deleteFromInventory = false)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRoute}/remove?{_Id}={Uri.EscapeDataString(id)}&{_DeleteFromInventory}={deleteFromInventory}&{_RemovalType}=Single";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveSubsetEdgebandEntities(string id, int quantity, bool deleteFromInventory = false)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRoute}/remove?{_Id}={Uri.EscapeDataString(id)}&{_DeleteFromInventory}={deleteFromInventory}&{_RemovalType}=Subset";

            await PatchObject(new Uri(url, UriKind.Relative));
        }

        #endregion Update

        #region Create

        /// TODO: Add correct parsing to the body
        /// <inheritdoc />
        public async Task<EdgebandEntity> CreateEdgebandEntity(string edgebandTypeCode, EdgebandEntity edgebandEntity)
        {
            if (string.IsNullOrEmpty(edgebandTypeCode))
            {
                throw new ArgumentException("Edgeband code must not be null or empty", nameof(edgebandTypeCode));
            }

            if (edgebandEntity.Length <= 0.1)
            {
                throw new ArgumentException("Length must be higher than 0.1", nameof(edgebandEntity.Length));
            }

            if (edgebandEntity.Quantity <= 0)
            {
                throw new ArgumentException("Quantity must be higher than 0", nameof(edgebandEntity.Quantity));
            }

            if (edgebandEntity.CurrentThickness <= 0.01)
            {
                throw new ArgumentException("Current thickness must be higher than 0.01", nameof(edgebandEntity.CurrentThickness));
            }

            var url =
                $"{_BaseRoute}?{_EdgebandTypeCode}={Uri.EscapeDataString(edgebandTypeCode)}";

            await PostObject(new Uri(url, UriKind.Relative), edgebandEntity);
            return new EdgebandEntity();
        }

        /// <inheritdoc />
        public async Task CreateEdgebandType(EdgebandType edgebandType)
        {
            var url = $"{_BaseRouteMaterialManager}/create";

            await PostObject(new Uri(url, UriKind.Relative), edgebandType);
        }

        #endregion Create
    }
}