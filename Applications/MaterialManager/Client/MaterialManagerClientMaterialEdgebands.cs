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
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Statistics;
using HomagConnect.MaterialManager.Contracts.Update;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager client for edgebands
/// </summary>
public class MaterialManagerClientMaterialEdgebands : ServiceBase, IMaterialManagerClientMaterialEdgebands
{
    private const string _BaseRoute = "api/materialManager/materials/edgebands";
    private const string _BaseStatisticsRoute = "api/materialManager/statistics";
    private const string _EdgebandCode = "edgebandCode";
    private const string _IncludingDetails = "includingDetails";

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandType>> GetEdgebandTypes(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}";

        return await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesIncludingDetails(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}&{_IncludingDetails}=true";

        return await RequestEnumerable<EdgebandTypeDetails>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<EdgebandType> GetEdgebandTypeByEdgebandCode(string edgebandCode)
    {
        return await GetEdgebandTypesByEdgebandCodes([edgebandCode]).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<EdgebandTypeDetails> GetEdgebandTypeByEdgebandCodeIncludingDetails(string edgebandCode)
    {
        return await GetEdgebandTypesByEdgebandCodesIncludingDetails([edgebandCode]).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandType>> GetEdgebandTypesByEdgebandCodes(IEnumerable<string> edgebandCodes)
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
        var boardTypes = new List<EdgebandType>();

        foreach (var url in urls)
        {
            boardTypes.AddRange(await RequestEnumerable<EdgebandType>(new Uri(url, UriKind.Relative)));
        }

        return boardTypes;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<EdgebandTypeDetails>> GetEdgebandTypesByEdgebandCodesIncludingDetails(IEnumerable<string> edgebandCodes)
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
        var edgebandTypeDetails = new List<EdgebandTypeDetails>();

        foreach (var url in urls)
        {
            edgebandTypeDetails.AddRange(await RequestEnumerable<EdgebandTypeDetails>(new Uri(url, UriKind.Relative)));
        }

        return edgebandTypeDetails;
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
    public async Task<IEnumerable<string>> GetTechnologyMacrosFromMachine(string tapioMachineId)
    {
        if (tapioMachineId == null)
        {
            throw new ArgumentNullException(nameof(tapioMachineId));
        }

        var url = $"{_BaseRoute}/macros?tapioMachineId={Uri.EscapeDataString(tapioMachineId)}";
        return await RequestEnumerable<string>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TapioMachine>> GetLicensedMachines()
    {
        const string url = $"{_BaseRoute}/machines";
        return await RequestEnumerable<TapioMachine>(new Uri(url, UriKind.Relative));
    }

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

        var payload = JsonConvert.SerializeObject(edgebandTypeUpdate);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PatchObject(new Uri(url, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<EdgebandType>(responseContent);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(edgebandCode)}");
    }

    #endregion Update

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

    #region Constructors

    /// <inheritdoc />
    public MaterialManagerClientMaterialEdgebands(HttpClient client) : base(client) { }

    /// <inheritdoc />
    public MaterialManagerClientMaterialEdgebands(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

    /// <inheritdoc />
    public MaterialManagerClientMaterialEdgebands(Guid subscriptionOrPartnerId, string authorizationKey, Uri? baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

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

        var payload = JsonConvert.SerializeObject(edgebandTypeRequest);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PostObject(new Uri(_BaseRoute, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<EdgebandType>(responseContent);

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
        var responseObject = JsonConvert.DeserializeObject<EdgebandType>(result);

        return responseObject ?? new EdgebandType();
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

    #endregion Delete
}