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
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Statistics;
using HomagConnect.MaterialManager.Contracts.Update;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager Client for boards
/// </summary>
public class MaterialManagerClientMaterialBoards : ServiceBase, IMaterialManagerClientMaterialBoards
{
    #region Update

    /// <inheritdoc />
    public async Task<BoardType> UpdateBoardType(string boardCode, MaterialManagerUpdateBoardType boardTypeUpdate)
    {
        if (boardTypeUpdate == null)
        {
            throw new ArgumentNullException(nameof(boardTypeUpdate));
        }

        ValidateRequiredProperties(boardTypeUpdate);

        var url = $"{_BaseRoute}?{_BoardCode}={Uri.EscapeDataString(boardCode)}";

        var payload = JsonConvert.SerializeObject(boardTypeUpdate, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PatchObject(new Uri(url, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BoardType>(responseContent, SerializerSettings.Default);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(BoardType)}");
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeAllocation>?> UpdateBoardTypeAllocation(string allocationName, BoardTypeAllocationUpdate boardTypeAllocationUpdate)
    {
        if (string.IsNullOrWhiteSpace(allocationName))
            throw new ArgumentException("Allocation name must not be null or empty.", nameof(allocationName));

        if (boardTypeAllocationUpdate == null)
            throw new ArgumentNullException(nameof(boardTypeAllocationUpdate));

        if (string.IsNullOrWhiteSpace(boardTypeAllocationUpdate.Name))
            throw new ArgumentException("The allocation update must have a valid Name.", nameof(boardTypeAllocationUpdate));

        var url = $"{_BoardTypeAllocationsRoute}?allocationName={Uri.EscapeDataString(allocationName)}";

        var payload = JsonConvert.SerializeObject(boardTypeAllocationUpdate, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");

        var response = await PatchObject(new Uri(url, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<IEnumerable<BoardTypeAllocation>>(responseContent, SerializerSettings.Default);

        return result;
    }

    #endregion Update

    #region Constructors

    /// <inheritdoc />
    public MaterialManagerClientMaterialBoards(HttpClient client) : base(client) { }

    /// <inheritdoc />
    public MaterialManagerClientMaterialBoards(Guid subscriptionOrPartnerId, string authorizationKey) : base(subscriptionOrPartnerId, authorizationKey) { }

    /// <inheritdoc />
    public MaterialManagerClientMaterialBoards(Guid subscriptionOrPartnerId, string authorizationKey, Uri baseUri) : base(subscriptionOrPartnerId, authorizationKey, baseUri) { }

    #endregion

    #region Create

    /// <inheritdoc />
    public async Task<BoardType> CreateBoardType(MaterialManagerRequestBoardType boardTypeRequest)
    {
        if (boardTypeRequest == null)
        {
            throw new ArgumentNullException(nameof(boardTypeRequest));
        }

        ValidateRequiredProperties(boardTypeRequest);

        var payload = JsonConvert.SerializeObject(boardTypeRequest, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PostObject(new Uri(_BaseRoute, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BoardType>(responseContent, SerializerSettings.Default);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(BoardType)}");
    }

    /// <inheritdoc />
    public async Task<BoardType> CreateBoardType(MaterialManagerRequestBoardType boardTypeRequest, FileReference[] fileReferences)
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

        var json = JsonConvert.SerializeObject(boardTypeRequest, SerializerSettings.Default);

        httpContent.Add(new StringContent(json), nameof(boardTypeRequest));

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
        var responseObject = JsonConvert.DeserializeObject<BoardType>(result, SerializerSettings.Default);

        return responseObject ?? new BoardType();
    }

    /// <inheritdoc />
    public async Task<BoardTypeAllocation> CreateBoardTypeAllocation(BoardTypeAllocationRequest boardTypeAllocationRequest)
    {
        if (boardTypeAllocationRequest == null)
        {
            throw new ArgumentNullException(nameof(boardTypeAllocationRequest));
        }

        ValidateRequiredProperties(boardTypeAllocationRequest);

        var payload = JsonConvert.SerializeObject(boardTypeAllocationRequest, SerializerSettings.Default);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var response = await PostObject(new Uri(_BoardTypeAllocationsRoute, UriKind.Relative), content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BoardTypeAllocation>(responseContent, SerializerSettings.Default);

        if (result != null)
        {
            return result;
        }

        throw new Exception($"The returned object is not of type {nameof(BoardTypeAllocation)}");
    }

    #endregion

    #region Constants

    private const string _BaseRoute = "api/materialManager/materials/boards";
    private const string _BoardTypeAllocationsRoute = _BaseRoute + "/allocations";
    private const string _BaseStatisticsRoute = "api/materialManager/statistics";
    private const string _MaterialCode = "materialCode";
    private const string _BoardCode = "boardCode";
    private const string _IncludingDetails = "includingDetails";

    #endregion

    #region Read

    /// <inheritdoc />
    public async Task<IEnumerable<BoardType>?> GetBoardTypes(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}";

        return await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeDetails>?> GetBoardTypesIncludingDetails(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}&{_IncludingDetails}=true";

        return await RequestEnumerable<BoardTypeDetails>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<BoardType?> GetBoardTypeByBoardCode(string boardCode)
    {
        return await GetBoardTypesByBoardCodes([boardCode]).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<BoardTypeDetails?> GetBoardTypeByBoardCodeIncludingDetails(string boardCode)
    {
        return await GetBoardTypesByBoardCodesIncludingDetails([boardCode]).FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardType?>> GetBoardTypesByBoardCodes(IEnumerable<string> boardCodes)
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
        var boardTypes = new List<BoardType?>();

        foreach (var url in urls)
        {
            boardTypes.AddRange(await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative)) ?? Array.Empty<BoardType>());
        }

        return boardTypes;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeDetails?>> GetBoardTypesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes)
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

        var urls = CreateUrls(codes, _BoardCode, includingDetails: true);
        var boardTypesDetails = new List<BoardTypeDetails?>();

        foreach (var url in urls)
        {
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(new Uri(url, UriKind.Relative)) ?? Array.Empty<BoardTypeDetails>());
        }

        return boardTypesDetails;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardType>?> GetBoardTypesByMaterialCode(string materialCode)
    {
        var url = $"{_BaseRoute}?{_MaterialCode}={Uri.EscapeDataString(materialCode)}";

        return await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeDetails>?> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
    {
        var url = $"{_BaseRoute}?{_MaterialCode}={Uri.EscapeDataString(materialCode)}&{_IncludingDetails}=true";

        return await RequestEnumerable<BoardTypeDetails>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes)
    {
        if (materialCodes == null)
        {
            throw new ArgumentNullException(nameof(materialCodes));
        }

        var codes = materialCodes
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct()
            .OrderBy(m => m)
            .ToList();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        var urls = CreateUrls(codes, _MaterialCode);
        var boardTypes = new List<BoardType>();

        foreach (var url in urls)
        {
            boardTypes.AddRange(await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative)) ?? Array.Empty<BoardType>());
        }

        return boardTypes;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes)
    {
        if (materialCodes == null)
        {
            throw new ArgumentNullException(nameof(materialCodes));
        }

        var codes = materialCodes
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct()
            .OrderBy(m => m)
            .ToList();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        var urls = CreateUrls(codes, _MaterialCode, includingDetails: true);
        var boardTypesDetails = new List<BoardTypeDetails>();

        foreach (var url in urls)
        {
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(new Uri(url, UriKind.Relative)) ?? Array.Empty<BoardTypeDetails>());
        }

        return boardTypesDetails;
    }

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

    /// <inheritdoc />
    public async Task<IEnumerable<Material>?> GetMaterials(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}/materials?take={take}&skip={skip}";
        return await RequestEnumerable<Material>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Material>?> GetMaterialsByMaterialCodes(IEnumerable<string> materialCodes)
    {
        if (materialCodes == null)
        {
            throw new ArgumentNullException(nameof(materialCodes));
        }

        var codes = materialCodes
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct()
            .OrderBy(m => m)
            .ToList();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        var urls = CreateUrls(codes, _MaterialCode, route: "/materials");
        var materials = new List<Material>();

        foreach (var url in urls)
        {
            materials.AddRange(await RequestEnumerable<Material>(new Uri(url, UriKind.Relative)) ?? []);
        }

        return materials;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeAllocation>?> GetBoardTypeAllocations(int take, int skip = 0)
    {
        var url = $"{_BoardTypeAllocationsRoute}?take={take}&skip={skip}";
        var result = await RequestEnumerable<BoardTypeAllocation>(new Uri(url, UriKind.Relative));
        return result ?? [];
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeAllocation>?> GetBoardTypeAllocationsByAllocationNames(IEnumerable<string> allocationNames, int take, int skip = 0)
    {
        if (allocationNames == null)
            throw new ArgumentNullException(nameof(allocationNames));

        var names = allocationNames
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        if (!names.Any())
            throw new ArgumentException("At least one allocation name must be passed.", nameof(allocationNames));

        if (take > 1000)
            throw new ArgumentException("The maximum value for 'take' is 1000.", nameof(take));

        var query = new StringBuilder($"?take={take}&skip={skip}");
        foreach (var name in names)
        {
            query.Append($"&allocationNames={Uri.EscapeDataString(name)}");
        }

        var url = $"{_BoardTypeAllocationsRoute}/byNames{query}";
        var result = await RequestEnumerable<BoardTypeAllocation>(new Uri(url, UriKind.Relative));
        return result ?? [];
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeAllocation>?> SearchBoardTypeAllocations(string search, int take, int skip = 0)
    {
        if (string.IsNullOrWhiteSpace(search))
            throw new ArgumentException("Search term must not be null or empty.", nameof(search));

        if (take > 1000)
            throw new ArgumentException("The maximum value for 'take' is 1000.", nameof(take));

        var url = $"{_BoardTypeAllocationsRoute}/search?search={Uri.EscapeDataString(search)}&take={take}&skip={skip}";
        return await RequestEnumerable<BoardTypeAllocation>(new Uri(url, UriKind.Relative));
    }

    #endregion

    #region Delete

    /// <inheritdoc />
    public async Task DeleteBoardType(string boardCode)
    {
        var url = $"{_BaseRoute}?{_BoardCode}={Uri.EscapeDataString(boardCode)}";

        await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteBoardTypes(IEnumerable<string> boardCodes)
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

        foreach (var url in urls)
        {
            await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
        }
    }

    /// <inheritdoc />
    public async Task DeleteBoardTypeAllocations(IEnumerable<string> allocationNames)
    {
        if (allocationNames == null)
            throw new ArgumentNullException(nameof(allocationNames));

        var names = allocationNames
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        if (!names.Any())
            throw new ArgumentException("At least one allocation name must be passed.", nameof(allocationNames));

        var query = new StringBuilder();
        foreach (var name in names)
        {
            query.Append(query.Length == 0 ? "?" : "&");
            query.Append($"allocationName={Uri.EscapeDataString(name)}");
        }

        var url = $"{_BoardTypeAllocationsRoute}{query}";

        await DeleteObject(new Uri(url, UriKind.Relative)).ConfigureAwait(false);
    }

    #endregion Delete

    #region statistics

    /// <inheritdoc />
    public Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, BoardTypeType boardTypeType, DateTime from, DateTime to)
    {
        if (materialCodes == null)
        {
            throw new ArgumentNullException(nameof(materialCodes));
        }

        var validMaterialCodes = materialCodes
            .Select(m => m.Trim())
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct()
            .OrderBy(m => m).ToList();

        if (!validMaterialCodes.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        return GetBoardTypeInventoryHistoryInternalAsync(validMaterialCodes, boardTypeType, from, to);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(DateTime from, DateTime to)
    {
        return await GetBoardTypeInventoryHistoryInternalAsync(null, null, from, to);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, DateTime from, DateTime to)
    {
        return await GetBoardTypeInventoryHistoryInternalAsync(materialCodes, null, from, to);
    }

    /// <inheritdoc />
    public Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, BoardTypeType boardTypeType, int daysBack)
    {
        return GetBoardTypeInventoryHistoryAsync(materialCodes, boardTypeType, DateTime.Now.AddDays(-daysBack), DateTime.Now);
    }

    /// <inheritdoc />
    public Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(int daysBack)
    {
        return GetBoardTypeInventoryHistoryAsync(DateTime.Now.AddDays(-daysBack), DateTime.Now);
    }

    /// <inheritdoc />
    public Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryAsync(IEnumerable<string> materialCodes, int daysBack)
    {
        return GetBoardTypeInventoryHistoryAsync(materialCodes, DateTime.Now.AddDays(-daysBack), DateTime.Now);
    }

    private async Task<IEnumerable<BoardTypeInventoryHistory>> GetBoardTypeInventoryHistoryInternalAsync(IEnumerable<string>? materialCodes, BoardTypeType? boardTypeType, DateTime from, DateTime to)
    {
        IEnumerable<String> paths;
        if (materialCodes != null)
        {
            paths = materialCodes
                .Select(materialCode => $"&materialCode={Uri.EscapeDataString(materialCode)}")
                .Join(QueryParametersMaxLength)
                .Select(c =>
                    $"/{_BaseStatisticsRoute}/inventory/boards?from={Uri.EscapeDataString(from.ToString("o", CultureInfo.InvariantCulture))}&to={Uri.EscapeDataString(to.ToString("o", CultureInfo.InvariantCulture))}" +
                    c);
        }
        else
        {
            paths =
            [
                $"/{_BaseStatisticsRoute}/inventory/boards?from={Uri.EscapeDataString(from.ToString("o", CultureInfo.InvariantCulture))}&to={Uri.EscapeDataString(to.ToString("o", CultureInfo.InvariantCulture))}"
            ];
        }

        if (boardTypeType != null)
        {
            paths = paths.Select(c => c + $"&boardTypeType={boardTypeType}");
        }

        return await RequestEnumerableAsync<BoardTypeInventoryHistory>(paths.Select(c => new Uri(c, UriKind.Relative)));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PartHistory>?> GetPartHistoryAsync(int daysBack, int take, int skip = 0)
    {
        var uri = $"/{_BaseStatisticsRoute}/usage/boards/parthistory?daysBack={daysBack}&take={take}&skip={skip}";

        return await RequestEnumerable<PartHistory>(new Uri(uri, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PartHistory>?> GetPartHistoryAsync(DateTime from, DateTime to, int take, int skip = 0)
    {
        var uri =
            $"/{_BaseStatisticsRoute}/usage/boards/parthistory?from={Uri.EscapeDataString(from.ToString("o", CultureInfo.InvariantCulture))}&to={Uri.EscapeDataString(to.ToString("o", CultureInfo.InvariantCulture))}&take={take}&skip={skip}";

        return await RequestEnumerable<PartHistory>(new Uri(uri, UriKind.Relative));
    }

    #endregion
}