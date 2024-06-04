using System.Text;

using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Base;
using HomagConnect.MaterialAssist.Contracts.Base.Enumerations;
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
        private const string _BaseRoute = "api/materialAssist/edgebands";
        private const string _BaseRouteMaterialManager = "api/materialManager/edgebands";
        private const string _EdgebandCode = "edgebandCode";
        private const string _IncludingDetails = "includingDetails";
        private const string _PrintLabel = "printLabel";
        private const string _FromStorage = "fromStorage";

        /// <inheritdoc />
        public MaterialAssistClientEdgebands(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>> GetEdgebandEntities(int take, int skip = 0)
        {
            var url = $"{_BaseRoute}?take={take}&skip={skip}";

            return await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<EdgebandEntity> GetEdgebandEntityById(string id)
        {
            var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(id)}";

            return await RequestObject<EdgebandEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCode(string edgebandCode)
        {
            var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}";

            return await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntityDetails>> GetEdgebandEntitiesByEdgebandCodeIncludingDetails(string edgebandCode)
        {
            var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}&{_IncludingDetails}=true";

            return await RequestEnumerable<EdgebandEntityDetails>(new Uri(url, UriKind.Relative));
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
        public async Task<IEnumerable<EdgebandEntity>> GetEdgebandEntitiesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes)
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

            var urls = CreateUrls(codes, _EdgebandCode, includingDetails:true);
            var edgebandDetailEntities = new List<EdgebandEntityDetails>();

            foreach (var url in urls)
            {
                edgebandDetailEntities.AddRange(await RequestEnumerable<EdgebandEntityDetails>(new Uri(url, UriKind.Relative)));
            }

            return edgebandDetailEntities;
        }

        /// <inheritdoc />
        public async Task<EdgebandEntityDetails> GetEdgebandEntityByIdIncludingDetails(string id)
        {
            var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(id)}&{_IncludingDetails}=true";

            return await RequestObject<EdgebandEntityDetails>(new Uri(url, UriKind.Relative));
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

            var urls = CreateUrls(codes, _EdgebandCode);
            var boardTypes = new List<EdgebandEntity>();

            foreach (var url in urls)
            {
                boardTypes.AddRange(await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative)));
            }

            return boardTypes;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntityDetails>> GetEdgebandEntitiesByIdsIncludingDetails(IEnumerable<string> ids)
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

            var urls = CreateUrls(codes, _EdgebandCode, includingDetails: true);
            var boardTypes = new List<EdgebandEntityDetails>();

            foreach (var url in urls)
            {
                boardTypes.AddRange(await RequestEnumerable<EdgebandEntityDetails>(new Uri(url, UriKind.Relative)));
            }

            return boardTypes;
        }

        /// <inheritdoc />
        public async Task UpdateEdgebandEntity(string id, double length, double currentThickness, string comments = "")
        {
            // if edgeband code is null or empty, throw an argument exception
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Edgeband code must not be null or empty", nameof(id));
            }

            if (length <= 0.1)
            {
                throw new ArgumentException("Length must be higher than 0.1", nameof(length));
            }

            if (currentThickness <= 0.01)
            {
                throw new ArgumentException("Current thickness must be higher than 0.01", nameof(currentThickness));
            }

            var commentsParam = string.IsNullOrWhiteSpace(comments) ? "" : $"&comments={Uri.EscapeDataString(comments)}";
            var url =
                $"{_BaseRoute}/update?{_EdgebandCode}={Uri.EscapeDataString(id)}&length={length}&currentThickness={currentThickness}{commentsParam}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task PrintLabel(string id)
        {
            var url = $"{_BaseRoute}/print?{_EdgebandCode}={id}&{_PrintLabel}=true";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DeleteEdgebandEntity(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Edgeband code must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRoute}?/delete?{_EdgebandCode}={Uri.EscapeDataString(id)}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StorageLocation>> GetStorageLocations()
        {
            var url = $"{_BaseRoute}/storageLocations";

            return await RequestEnumerable<StorageLocation>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task StoreEdgebandEntity(string id, string storageLocation, double length)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Edgeband code must not be null or empty", nameof(id));
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
                $"{_BaseRoute}/store?{_EdgebandCode}={Uri.EscapeDataString(id)}&storageLocation={storageLocation}&length={length}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task RemoveEdgebandEntity(string id, bool deleteFromInventory = false)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Edgeband code must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRoute}/remove?{_EdgebandCode}={Uri.EscapeDataString(id)}&deleteFromInventory={deleteFromInventory}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        /// TODO: Add correct parsing to the body
        /// <inheritdoc />
        public async Task CreateEdgebandEntity(string edgebandTypeCode, double length, int quantity, double currentThickness,
            ManagementType managementType,
            string comments, StorageLocation storageLocation)
        {
            if (string.IsNullOrEmpty(edgebandTypeCode))
            {
                throw new ArgumentException("Edgeband code must not be null or empty", nameof(edgebandTypeCode));
            }

            if (length <= 0.1)
            {
                throw new ArgumentException("Length must be higher than 0.1", nameof(length));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be higher than 0", nameof(quantity));
            }

            if (currentThickness <= 0.01)
            {
                throw new ArgumentException("Current thickness must be higher than 0.01", nameof(currentThickness));
            }

            var commentsParam = string.IsNullOrWhiteSpace(comments) ? "" : $"&comments={Uri.EscapeDataString(comments)}";
            var shelfIdParam = string.IsNullOrWhiteSpace(storageLocation)
                ? ""
                : $"&storageLocation={Uri.EscapeDataString(storageLocation)}";
            var url =
                $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandTypeCode)}&length={length}&quantity={quantity}&currentThickness={currentThickness}&management={managementType}{commentsParam}{shelfIdParam}";

            await PostObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task CreateEdgebandType(EdgebandType edgebandType)
        {
            var url = $"{_BaseRouteMaterialManager}/create";

            await PostObject(new Uri(url, UriKind.Relative), edgebandType);
        }

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
    }
}