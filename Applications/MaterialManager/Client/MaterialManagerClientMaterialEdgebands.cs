using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base;
using HomagConnect.Base.Contracts;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Common;
using HomagConnect.MaterialManager.Contracts.Delete;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Statistics;
using HomagConnect.MaterialManager.Contracts.Update;

using Newtonsoft.Json;

// ReSharper disable LocalizableElement

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager client for edgebands
/// </summary>
public class MaterialManagerClientMaterialEdgebands : ServiceBase, IMaterialManagerClientMaterialEdgebands
{
    private const string _BaseRoute = "api/materialManager/materials/edgebands";
    private const string _EdgebandTypeAllocations = _BaseRoute + "/allocations";

    private const string _BaseStatisticsRoute = "api/materialManager/statistics";
    private const string _EdgebandCode = "edgebandCode";
    private const string _IncludingDetails = "includingDetails";

    #region Private Methods

    private static List<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "",
        bool includingDetails = false)
    {
        var urls = codes
            .Select(code => $"&{searchCode}={Uri.EscapeDataString(code)}")
            .Join(QueryParametersMaxLength)
            .Select(x => x.Remove(0, 1).Insert(0, "?"))
            .Select(parameter => includingDetails ? $"{_BaseRoute}{route}" + parameter + $"&{_IncludingDetails}=true" : $"{_BaseRoute}{route}" + parameter).ToList();
        return urls;
    }

    #endregion Private methods

    #region Get

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandType>?> GetEdgebandTypes(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}";

        return await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandType>?> GetEdgebandTypes(DateTimeOffset changedSince, int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}&{nameof(changedSince)}={Uri.EscapeDataString(changedSince.ToString("o", CultureInfo.InvariantCulture))}";
        return await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandTypeDetails>?> GetEdgebandTypesIncludingDetails(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}&{_IncludingDetails}=true";

        return await RequestEnumerable<EdgebandTypeDetails>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandTypeDetails>?> GetEdgebandTypesIncludingDetails(DateTimeOffset changedSince, int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}&{_IncludingDetails}=true&{nameof(changedSince)}={Uri.EscapeDataString(changedSince.ToString("o", CultureInfo.InvariantCulture))}";
        return await RequestEnumerable<EdgebandTypeDetails>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<EdgebandType?> GetEdgebandTypeByEdgebandCode(string edgebandCode)
    {
        return await GetEdgebandTypesByEdgebandCodes([edgebandCode]).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<EdgebandTypeDetails?> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode)
    {
        return await GetEdgebandTypesByEdgebandCodesIncludingDetails([edgebandCode]).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandType?>> GetEdgebandTypesByEdgebandCodes(IEnumerable<string> edgebandCodes)
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
        var boardTypes = new List<EdgebandType?>();

        foreach (var url in urls)
        {
            boardTypes.AddRange(await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative)) ?? []);
        }

        return boardTypes;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandTypeDetails?>> GetEdgebandTypesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes)
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

        var urls = CreateUrls(codes, _EdgebandCode, includingDetails: true);
        var edgebandTypeDetails = new List<EdgebandTypeDetails?>();

        foreach (var url in urls)
        {
            edgebandTypeDetails.AddRange(await RequestEnumerable<EdgebandTypeDetails>(new Uri(url, UriKind.Relative)) ?? []);
        }

        return edgebandTypeDetails;
    }

    /// <inheritdoc />
    public async Task<EdgebandTypeAllocation> GetEdgebandTypeAllocation(string order, string customer, string project, string edgebandCode)
    {
        if (string.IsNullOrWhiteSpace(order))
        {
            throw new ArgumentException("Order must not be null or empty.", nameof(order));
        }

        if (string.IsNullOrWhiteSpace(customer))
        {
            throw new ArgumentException("Customer must not be null or empty.", nameof(customer));
        }

        if (string.IsNullOrWhiteSpace(project))
        {
            throw new ArgumentException("Project must not be null or empty.", nameof(project));
        }

        if (string.IsNullOrWhiteSpace(edgebandCode))
        {
            throw new ArgumentException("EdgebandCode must not be null or empty.", nameof(edgebandCode));
        }

        // Build the query string for the allocation
        var url = $"{_EdgebandTypeAllocations}?" +
                  $"order={Uri.EscapeDataString(order)}" +
                  $"&customer={Uri.EscapeDataString(customer)}" +
                  $"&project={Uri.EscapeDataString(project)}" +
                  $"&edgebandCode={Uri.EscapeDataString(edgebandCode)}";

        var response = await RequestObject<EdgebandTypeAllocation>(new Uri(url, UriKind.Relative));
        if (response != null)
        {
            return response;
        }

        throw new Exception("EdgebandTypeAllocation not found for the specified parameters.");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandTypeAllocation>> GetEdgebandTypeAllocations()
    {
        var url = $"{_EdgebandTypeAllocations}/getall";
        var result = await RequestEnumerable<EdgebandTypeAllocation>(new Uri(url, UriKind.Relative));
        return result ?? [];
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandTypeAllocation>> GetEdgebandTypeAllocations(DateTimeOffset changedSince)
    {
        var url = $"{_EdgebandTypeAllocations}?changedSince={Uri.EscapeDataString(changedSince.ToString("o", CultureInfo.InvariantCulture))}";
        var result = await RequestEnumerable<EdgebandTypeAllocation>(new Uri(url, UriKind.Relative));
        return result ?? [];
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(DateTime from, DateTime to)
    {
        List<Uri> requestUri =
        [
            new Uri(
                $"/{_BaseStatisticsRoute}/inventory/edgebands?from={Uri.EscapeDataString(from.ToString("o", CultureInfo.InvariantCulture))}&to={Uri.EscapeDataString(to.ToString("o", CultureInfo.InvariantCulture))}",
                UriKind.Relative)
        ];
        var ret = await RequestEnumerableAsync<EdgeInventoryHistory>(requestUri);
        return ret;
    }

    /// <inheritdoc />
    public Task<IEnumerable<EdgeInventoryHistory>> GetEdgebandTypeInventoryHistoryAsync(int daysBack)
    {
        return GetEdgebandTypeInventoryHistoryAsync(DateTime.Now.AddDays(-daysBack), DateTime.Now);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<string>?> GetTechnologyMacrosFromMachine(string tapioMachineId)
    {
        if (tapioMachineId == null)
        {
            throw new ArgumentNullException(nameof(tapioMachineId));
        }

        var url = $"{_BaseRoute}/machines/{Uri.EscapeDataString(tapioMachineId)}/macros";
        return await RequestEnumerable<string>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TapioMachine>?> GetLicensedMachines()
    {
        const string url = $"{_BaseRoute}/machines";
        return await RequestEnumerable<TapioMachine>(new Uri(url, UriKind.Relative));
    }

    #endregion Get

    #region Update

    /// <inheritdoc />
    public async Task<EdgebandType> UpdateEdgebandType(string edgebandCode, MaterialManagerUpdateEdgebandType edgebandTypeUpdate)
    {
        if (edgebandTypeUpdate == null)
        {
            throw new ArgumentNullException(nameof(edgebandTypeUpdate));
        }

        ValidateRequiredProperties(edgebandTypeUpdate);

        var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}";

        var payload = JsonConvert.SerializeObject(edgebandTypeUpdate, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PatchObject(new Uri(url, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<EdgebandType>(responseContent, SerializerSettings.Default);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(edgebandCode)}");
    }

    /// <inheritdoc />
    public async Task<EdgebandTypeAllocation> UpdateEdgebandTypeAllocation(EdgebandTypeAllocationUpdate edgebandTypeAllocationUpdate)
    {
        if (edgebandTypeAllocationUpdate == null)
        {
            throw new ArgumentNullException(nameof(edgebandTypeAllocationUpdate));
        }

        ValidateRequiredProperties(edgebandTypeAllocationUpdate);

        // The endpoint for updating allocations is assumed to be /allocations
        var url = $"{_EdgebandTypeAllocations}";

        var payload = JsonConvert.SerializeObject(edgebandTypeAllocationUpdate, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PatchObject(new Uri(url, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<EdgebandTypeAllocation>(responseContent, SerializerSettings.Default);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(EdgebandTypeAllocation)}");
    }

    #endregion Update

    #region Constructors

    /// <inheritdoc />
    public MaterialManagerClientMaterialEdgebands(HttpClient client) : base(client) { }

    /// <inheritdoc />
    public MaterialManagerClientMaterialEdgebands(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

    /// <inheritdoc />
    public MaterialManagerClientMaterialEdgebands(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

    #endregion

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
        var response = await PostObject(new Uri(_BaseRoute, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<EdgebandType>(responseContent, SerializerSettings.Default);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(EdgebandType)}");
    }

    /// <inheritdoc />
    public async Task<EdgebandType> CreateEdgebandType(MaterialManagerRequestEdgebandType edgebandTypeRequest, FileReference[] fileReferences)
    {
        if (fileReferences == null)
        {
            throw new ArgumentNullException(nameof(fileReferences));
        }

        var missingFile = fileReferences.FirstOrDefault(f => !f.FileInfo.Exists);

        if (missingFile != null)
        {
            throw new FileNotFoundException($"File '{missingFile.FileInfo.FullName}' was not found.");
        }

        var missingReference = fileReferences.FirstOrDefault(f => string.IsNullOrWhiteSpace(f.Reference));

        if (missingReference != null)
        {
            throw new ArgumentException($"Reference for file '{missingReference.FileInfo.FullName}' is missing.");
        }

        var request = new HttpRequestMessage { Method = HttpMethod.Post };
        request.RequestUri = new Uri(_BaseRoute, UriKind.Relative);

        using var httpContent = new MultipartFormDataContent();

        var json = JsonConvert.SerializeObject(edgebandTypeRequest, SerializerSettings.Default);

        httpContent.Add(new StringContent(json), nameof(edgebandTypeRequest));

        foreach (var fileReference in fileReferences)
        {
            var fileStream = fileReference.FileInfo.OpenRead();

            HttpContent streamContent = new StreamContent(fileStream);
            httpContent.Add(streamContent, fileReference.Reference, fileReference.FileInfo.Name);
        }

        request.Content = httpContent;

        var response = await Client.SendAsync(request);

        await response.EnsureSuccessStatusCodeWithDetailsAsync(request);

        var result = await response.Content.ReadAsStringAsync();
        var responseObject = JsonConvert.DeserializeObject<EdgebandType>(result, SerializerSettings.Default);

        return responseObject ?? new EdgebandType();
    }

    /// <inheritdoc />
    public async Task<EdgebandTypeAllocation> CreateEdgebandTypeAllocation(EdgebandTypeAllocationRequest edgebandTypeAllocationRequest)
    {
        if (edgebandTypeAllocationRequest == null)
        {
            throw new ArgumentNullException(nameof(edgebandTypeAllocationRequest));
        }

        ValidateRequiredProperties(edgebandTypeAllocationRequest);

        var payload = JsonConvert.SerializeObject(edgebandTypeAllocationRequest, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PostObject(new Uri(_EdgebandTypeAllocations, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<EdgebandTypeAllocation>(responseContent, SerializerSettings.Default);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(EdgebandTypeAllocation)}");
    }

    #endregion Create

    #region Delete

    /// <inheritdoc />
    public async Task DeleteEdgebandType(string edgebandCode)
    {
        var url = $"{_BaseRoute}?{_EdgebandCode}={Uri.EscapeDataString(edgebandCode)}";

        await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteEdgebandTypes(IEnumerable<string> edgebandCodes)
    {
        if (edgebandCodes == null)
        {
            throw new ArgumentNullException(nameof(edgebandCodes));
        }

        var codes = edgebandCodes
            .Where(b => !string.IsNullOrWhiteSpace(b))
            .Distinct()
            .OrderBy(b => b).ToList();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(edgebandCodes), "At least one board code must be passed.");
        }

        var urls = CreateUrls(codes, _EdgebandCode);

        foreach (var url in urls)
        {
            await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task DeleteEdgebandTypeAllocation(EdgebandTypeAllocationDelete edgebandTypeAllocationDelete)
    {
        if (edgebandTypeAllocationDelete == null)
        {
            throw new ArgumentNullException(nameof(edgebandTypeAllocationDelete));
        }

        // The endpoint for deleting allocations is assumed to be /allocations
        var url = $"{_EdgebandTypeAllocations}";

        var payload = JsonConvert.SerializeObject(edgebandTypeAllocationDelete, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        // Typically, delete with body uses HttpMethod.Delete and content
        var request = new HttpRequestMessage(HttpMethod.Delete, new Uri(url, UriKind.Relative))
        {
            Content = content
        };

        var response = await Client.SendAsync(request);
        await response.EnsureSuccessStatusCodeWithDetailsAsync(request);
    }

    #endregion Delete
}