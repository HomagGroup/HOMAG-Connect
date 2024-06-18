using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using HomagConnect.MaterialManager.Contracts.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HomagConnect.MaterialManager.Client;

/// <summary>
/// materialManager Client for boards
/// </summary>
public class MaterialManagerClientMaterialBoards : ServiceBase, IMaterialManagerClientMaterialBoards
{
    #region Constants

    private const string _BaseRoute = "api/materialManager/materials/boards";
    private const string _BaseStatisticsRoute = "api/materialManager/statistics";
    private const string _MaterialCode = "materialCode";
    private const string _BoardCode = "boardCode";
    private const string _IncludingDetails = "includingDetails";

    #endregion

    #region Read
    /// <inheritdoc />
    public MaterialManagerClientMaterialBoards(HttpClient client) : base(client) { }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardType>> GetBoardTypes(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}";

        return await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<BoardType> GetBoardTypeByBoardCode(string boardCode)
    {
        var url = $"{_BaseRoute}?{_BoardCode}={Uri.EscapeDataString(boardCode)}";

        return await RequestObject<BoardType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<BoardTypeDetails> GetBoardTypeByBoardCodeIncludingDetails(string boardCode)
    {
        var url = $"{_BaseRoute}?{_BoardCode}={Uri.EscapeDataString(boardCode)}&{_IncludingDetails}=true";

        return await RequestObject<BoardTypeDetails>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardType>> GetBoardTypesByBoardCodes(IEnumerable<string> boardCodes)
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
            throw new ArgumentNullException(nameof(boardCodes), "At least one board code code must be passed.");
        }

        var urls = CreateUrls(codes, _BoardCode);
        var boardTypes = new List<BoardType>();

        foreach (var url in urls)
        {
            boardTypes.AddRange(await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative)));
        }

        return boardTypes;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByBoardCodesIncludingDetails(IEnumerable<string> boardCodes)
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
            throw new ArgumentNullException(nameof(boardCodes), "At least one board code code must be passed.");
        }

        var urls = CreateUrls(codes, _BoardCode, includingDetails: true);
        var boardTypesDetails = new List<BoardTypeDetails>();

        foreach (var url in urls)
        {
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(new Uri(url, UriKind.Relative)));
        }

        return boardTypesDetails;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardType>> GetBoardTypesByMaterialCode(string materialCode)
    {
        var url = $"{_BaseRoute}?{_MaterialCode}={Uri.EscapeDataString(materialCode)}";

        return await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative));
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
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
            boardTypes.AddRange(await RequestEnumerable<BoardType>(new Uri(url, UriKind.Relative)));
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
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(new Uri(url, UriKind.Relative)));
        }

        return boardTypesDetails;
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
    #endregion

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
        return GetBoardTypeInventoryHistoryAsync( DateTime.Now.AddDays(-daysBack), DateTime.Now);
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
                .Select(c => $"/{_BaseStatisticsRoute}/inventory/boards?from={from:s}&to={to:s}" + c);
        }
        else
        {
            paths = [$"/{_BaseStatisticsRoute}/inventory/boards?from={from:s}&to={to:s}"];
        }

        if (boardTypeType != null)
        {
            paths = paths.Select(c => c + $"&boardTypeType={boardTypeType}");
        }

        return await RequestEnumerableAsync<BoardTypeInventoryHistory>(paths.Select(c => new Uri(c, UriKind.Relative)));
    }
    #endregion
}