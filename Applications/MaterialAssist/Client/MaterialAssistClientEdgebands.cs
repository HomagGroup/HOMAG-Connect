using System.Text;

using HomagConnect.Base;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.MaterialAssist.Contracts.Edgebands;
using HomagConnect.MaterialAssist.Contracts.Request;
using HomagConnect.MaterialAssist.Contracts.Storage;
using HomagConnect.MaterialAssist.Contracts.Update;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Request;

using Newtonsoft.Json;

namespace HomagConnect.MaterialAssist.Client
{
    /// <summary>
    /// materialAssist client for edgebands
    /// </summary>
    public class MaterialAssistClientEdgebands : ServiceBase, IMaterialAssistClientEdgebands
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

        #region Create

        /// <inheritdoc />
        public async Task<EdgebandType> CreateEdgebandType(MaterialManagerRequestEdgebandType edgebandTypeRequest)
        {
            if (edgebandTypeRequest == null)
            {
                throw new ArgumentNullException(nameof(edgebandTypeRequest));
            }

            ValidateRequiredProperties(edgebandTypeRequest);

            var payload = JsonConvert.SerializeObject(edgebandTypeRequest, SerializerSettings.Default);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await PostObject(new Uri(_BaseRouteMaterialManager, UriKind.Relative), content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EdgebandType>(responseContent, SerializerSettings.Default);

            if (result != null)
            {
                return result;
            }

            throw new Exception($"The returned object is not of type {nameof(EdgebandType)}");
        }

        /// <inheritdoc />
        public async Task<EdgebandEntity> CreateEdgebandEntity(MaterialAssistRequestEdgebandEntity edgebandEntityRequest)
        {
            if (edgebandEntityRequest == null)
            {
                throw new ArgumentNullException(nameof(edgebandEntityRequest));
            }

            ValidateRequiredProperties(edgebandEntityRequest);

            var payload = JsonConvert.SerializeObject(edgebandEntityRequest, SerializerSettings.Default);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await PostObject(new Uri(_BaseRouteMaterialAssist, UriKind.Relative), content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EdgebandEntity>(responseContent, SerializerSettings.Default);

            if (result != null)
            {
                return result;
            }

            throw new Exception($"The returned object is not of type {nameof(EdgebandEntity)}");
        }

        #endregion Create

        #region Constructors

        /// <inheritdoc />
        public MaterialAssistClientEdgebands(HttpClient client) : base(client) { }

        /// <inheritdoc />
        public MaterialAssistClientEdgebands(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

        /// <inheritdoc />
        public MaterialAssistClientEdgebands(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

        #endregion

        #region Delete

        /// <inheritdoc />
        public async Task DeleteEdgebandEntity(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRouteMaterialAssist}?{_Id}={Uri.EscapeDataString(id)}";

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task DeleteEdgebandEntity(IEnumerable<string> ids)
        {
            var url = $"{_BaseRouteMaterialAssist}";

            var edgebandCodes = new StringBuilder("?");
            edgebandCodes.Append(string.Join("&", ids.Select(id => $"{_Id}={Uri.EscapeDataString(id)}")));

            url += edgebandCodes;

            await DeleteObject(new Uri(url, UriKind.Relative));
        }

        #endregion Delete

        #region Constants

        private const string _BaseRouteMaterialAssist = "api/materialAssist/edgebandEntities";
        private const string _BaseRouteMaterialManager = "api/materialManager/edgebands";
        private const string _Id = "Id";
        private const string _EdgebandCode = "edgebandCode";
        private const string _StorageLocation = "storageLocation";
        private const string _DeleteFromInventory = "deleteFromInventory";
        private const string _Length = "length";
        private const string _RemovalType = "removalType";
        private const string _Quantity = "quantity";

        #endregion Constants

        #region Read

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>?> GetEdgebandEntities(int take, int skip = 0)
        {
            var url = $"{_BaseRouteMaterialAssist}?take={take}&skip={skip}";

            return await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<EdgebandEntity?> GetEdgebandEntityById(string id)
        {
            return await GetEdgebandEntitiesByIds([id]).FirstOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity?>> GetEdgebandEntitiesByIds(IEnumerable<string> ids)
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
                throw new ArgumentNullException(nameof(ids), "At least one id must be passed.");
            }

            var urls = CreateUrls(codes, _Id);
            var edgebandEntities = new List<EdgebandEntity?>();

            foreach (var url in urls)
            {
                edgebandEntities.AddRange(await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative)) ?? Array.Empty<EdgebandEntity>());
            }

            return edgebandEntities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EdgebandEntity>?> GetEdgebandEntitiesByEdgebandCode(string edgebandCode)
        {
            var url = $"{_BaseRouteMaterialAssist}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}";

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
                throw new ArgumentNullException(nameof(edgebandCodes), "At least one edgeband code must be passed.");
            }

            var urls = CreateUrls(codes, _EdgebandCode);
            var edgebandEntities = new List<EdgebandEntity>();

            foreach (var url in urls)
            {
                edgebandEntities.AddRange(await RequestEnumerable<EdgebandEntity>(new Uri(url, UriKind.Relative)) ?? Array.Empty<EdgebandEntity>());
            }

            return edgebandEntities;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StorageLocation>> GetStorageLocations()
        {
            const string url = $"{_BaseRouteMaterialAssist}/storageLocations";

            return await RequestEnumerable<StorageLocation>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<StorageLocation>> GetStorageLocations(string workstationId)
        {
            var url = $"{_BaseRouteMaterialAssist}/storageLocations?workstationId={Uri.EscapeDataString(workstationId)}";

            return await RequestEnumerable<StorageLocation>(new Uri(url, UriKind.Relative));
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Workstation>> GetWorkstations()
        {
            var url = $"{_BaseRouteMaterialAssist}/workstations";

            return await RequestEnumerable<Workstation>(new Uri(url, UriKind.Relative));
        }

        #endregion Read

        #region Update

        /// <inheritdoc />
        public async Task<EdgebandEntity> UpdateEdgebandEntity(string id, MaterialAssistUpdateEdgebandEntity updateEdgebandEntity)
        {
            if (updateEdgebandEntity == null)
            {
                throw new ArgumentNullException(nameof(updateEdgebandEntity));
            }

            ValidateRequiredProperties(updateEdgebandEntity);

            var url = $"{_BaseRouteMaterialAssist}?{_Id}={Uri.EscapeDataString(id)}";

            var payload = JsonConvert.SerializeObject(updateEdgebandEntity, SerializerSettings.Default);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await PatchObject(new Uri(url, UriKind.Relative), content);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EdgebandEntity>(responseContent, SerializerSettings.Default);

            if (result != null)
            {
                return result;
            }

            throw new Exception($"The returned object is not of type {nameof(EdgebandEntity)}");
        }

        public async Task StoreEdgebandEntity(MaterialAssistStoreEdgebandEntity storeEdgebandEntity)
        {
            if (storeEdgebandEntity == null)
            {
                throw new ArgumentNullException(nameof(storeEdgebandEntity));
            }

            if (string.IsNullOrEmpty(storeEdgebandEntity.Id))
            {
                throw new ArgumentException("Id must not be null or empty.", nameof(storeEdgebandEntity.Id));
            }

            if (storeEdgebandEntity.Length is <= 0.1 or >= 9999.99)
            {
                throw new ArgumentException("Length must be between 0.1 and 9999.99.", nameof(storeEdgebandEntity.Length));
            }

            if (storeEdgebandEntity.StorageLocation == null)
            {
                throw new ArgumentException("Storage location must be provided.", nameof(storeEdgebandEntity.StorageLocation));
            }

            if (storeEdgebandEntity.Workstation == null)
            {
                throw new ArgumentException("Workstation must be provided.", nameof(storeEdgebandEntity.Workstation));
            }

            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(storeEdgebandEntity.Id)}/store";

            var payload = JsonConvert.SerializeObject(storeEdgebandEntity, SerializerSettings.Default);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await PostObject(new Uri(url, UriKind.Relative), content).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to store edgeband entity. Status: {response.StatusCode}, Response: {responseContent}");
            }
        }

        [Obsolete($"This method is obsolete. Use the method with {nameof(MaterialAssistStoreEdgebandEntity)} instead.", true)]
        public Task StoreEdgebandEntity(string id, StorageLocation storageLocation, double length)
        {
            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public Task RemoveAllEdgebandEntitiesFromWorkplace(string id, bool deleteFromInventory = false)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(id)}/remove?{_DeleteFromInventory}={deleteFromInventory}&{_RemovalType}=All";

            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public Task RemoveSingleEdgebandEntitiesFromWorkplace(string id, int quantity, bool deleteFromInventory = false)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(id)}/remove?{_Quantity}={quantity}&{_DeleteFromInventory}={deleteFromInventory}&{_RemovalType}=Single";

            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        /// <inheritdoc />
        public Task RemoveSubsetEdgebandEntitiesFromWorkplace(string id, int quantity, bool deleteFromInventory = false)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Id must not be null or empty", nameof(id));
            }

            var url = $"{_BaseRouteMaterialAssist}/{Uri.EscapeDataString(id)}/remove?{_Quantity}={quantity}&{_DeleteFromInventory}={deleteFromInventory}&{_RemovalType}=Subset";

            throw new NotImplementedException("This feature is going to be implemented in the future", new Exception());
        }

        #endregion Update
    }
}