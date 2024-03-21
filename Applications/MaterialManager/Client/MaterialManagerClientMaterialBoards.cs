using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Extensions;
using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialManager.Client;

public class MaterialManagerClientMaterialBoards : ServiceBase, IMaterialManagerClientMaterialBoards
{
    #region Constants

    private const string _BaseRoute = "api/materialManager/materials/boards";
    private const string _InventoryRoute = "/inventory";
    private const string _MaterialCodesRoute = "/materialCodes";
    private const string _MaterialCode = "materialCode";
    private const string _BoardCode = "boardCode";
    private const string _IncludingDetails = "includingDetails";

    #endregion

    #region Read

    public MaterialManagerClientMaterialBoards(HttpClient client) : base(client) { }

    /// <inheritdoc />
    public async Task<BoardType> GetBoardType(string boardCode)
    {
        var url = $"{_BaseRoute}?{_BoardCode}={Uri.EscapeDataString(boardCode)}";

        return await RequestObject<BoardType>(url);
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardCodeWithInventory> GetBoardTypeInventory(IEnumerable<string> boardCodes)
    {
        if (boardCodes == null)
        {
            throw new ArgumentNullException(nameof(boardCodes));
        }

        var codes = boardCodes
            .Where(b => !string.IsNullOrWhiteSpace(b))
            .Distinct()
            .OrderBy(b => b).AsEnumerated();

        var urls = CreateUrls(codes, _BoardCode, _InventoryRoute);

        var boardCodesWithInventory = new List<BoardCodeWithInventory>();

        foreach (var url in urls)
        {
            boardCodesWithInventory.AddRange(await RequestEnumerable<BoardCodeWithInventory>(url));
        }

        foreach (var boardCode in boardCodesWithInventory)
        {
            yield return boardCode;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardType> GetBoardTypes(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}?take={take}&skip={skip}";

        var boardTypes = await RequestEnumerable<BoardType>(url);

        foreach (var boardType in boardTypes)
        {
            yield return boardType;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardType> GetBoardTypes(IEnumerable<string> boardCodes)
    {
        if (boardCodes == null)
        {
            throw new ArgumentNullException(nameof(boardCodes));
        }

        var codes = boardCodes
            .Where(b => !string.IsNullOrWhiteSpace(b))
            .Distinct()
            .OrderBy(b => b).AsEnumerated();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(boardCodes), "At least one board code code must be passed.");
        }

        var urls = CreateUrls(codes, _BoardCode);

        var boardTypes = new List<BoardType>();

        foreach (var url in urls)
        {
            boardTypes.AddRange(await RequestEnumerable<BoardType>(url));
        }

        foreach (var boardType in boardTypes)
        {
            yield return boardType;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardType> GetBoardTypesByMaterialCode(string materialCode)
    {
        var url = $"{_BaseRoute}?{_MaterialCode}={Uri.EscapeDataString(materialCode)}";

        var boardTypes = await RequestEnumerable<BoardType>(url);

        foreach (var boardType in boardTypes)
        {
            yield return boardType;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardTypeDetails> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
    {
        var url = $"{_BaseRoute}?{_MaterialCode}={Uri.EscapeDataString(materialCode)}&{_IncludingDetails}=true";

        var boardTypesDetails = await RequestEnumerable<BoardTypeDetails>(url);

        foreach (var boardTypesDetail in boardTypesDetails)
        {
            yield return boardTypesDetail;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardType> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes)
    {
        if (materialCodes == null)
        {
            throw new ArgumentNullException(nameof(materialCodes));
        }

        var codes = materialCodes
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct()
            .OrderBy(m => m)
            .AsEnumerated();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        var urls = CreateUrls(codes, _MaterialCode);

        var boardCodesWithInventory = new List<BoardType>();

        foreach (var url in urls)
        {
            boardCodesWithInventory.AddRange(await RequestEnumerable<BoardType>(url));
        }

        foreach (var boardCodeWithInventory in boardCodesWithInventory)
        {
            yield return boardCodeWithInventory;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardTypeDetails> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes)
    {
        if (materialCodes == null)
        {
            throw new ArgumentNullException(nameof(materialCodes));
        }

        var codes = materialCodes
            .Where(m => !string.IsNullOrWhiteSpace(m))
            .Distinct()
            .OrderBy(m => m)
            .AsEnumerated();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        var uris = CreateUrls(codes, _MaterialCode, includingDetails: true);

        var boardTypesDetails = new List<BoardTypeDetails>();

        foreach (var uri in uris)
        {
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(uri));
        }

        foreach (var boardTypeDetails in boardTypesDetails)
        {
            yield return boardTypeDetails;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<BoardTypeDetails> GetBoardTypesIncludingDetails(IEnumerable<string> boardCodes)
    {
        if (boardCodes == null)
        {
            throw new ArgumentNullException(nameof(boardCodes));
        }

        var codes = boardCodes
            .Where(b => !string.IsNullOrWhiteSpace(b))
            .Distinct()
            .OrderBy(b => b).AsEnumerated();

        if (!codes.Any())
        {
            throw new ArgumentNullException(nameof(boardCodes), "At least one board code code must be passed.");
        }

        var urls = CreateUrls(codes, _BoardCode, includingDetails: true);

        var boardTypesDetails = new List<BoardTypeDetails>();

        foreach (var url in urls)
        {
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(url));
        }

        foreach (var boardTypeDetails in boardTypesDetails)
        {
            yield return boardTypeDetails;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<MaterialCodeWithThumbnail> GetMaterialCodes(string search, int take, int skip = 0)
    {
        var url = $"{_BaseRoute}{_MaterialCodesRoute}?search={Uri.EscapeDataString(search)}&take={take}&skip={skip}";

        var materialCodesWithThumbnail = await RequestEnumerable<MaterialCodeWithThumbnail>(url);

        foreach (var materialCodeWithThumbnail in materialCodesWithThumbnail)
        {
            yield return materialCodeWithThumbnail;
        }
    }

    /// <inheritdoc />
    public async IAsyncEnumerable<MaterialCodeWithThumbnail> GetMaterialCodes(int take, int skip = 0)
    {
        var url = $"{_BaseRoute}{_MaterialCodesRoute}?take={take}&skip={skip}";

        var materialCodesWithThumbnail = await RequestEnumerable<MaterialCodeWithThumbnail>(url);

        foreach (var materialCodeWithThumbnail in materialCodesWithThumbnail)
        {
            yield return materialCodeWithThumbnail;
        }
    }

    private static IEnumerable<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "",
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
            if (queryParameters.Length + _BaseRoute.Length > 1900)
            {
                if (includingDetails)
                {
                    urls.Add($"{_BaseRoute}{route}{queryParameters}&{_IncludingDetails}=true");
                }
                else
                {
                    urls.Add($"{_BaseRoute}{route}{queryParameters}");
                }

                queryParameters = new StringBuilder("?");
            }

            i++;
            if (i <= codeList.Count)
            {
                queryParameters.Append("&");
            }
        }

        if (includingDetails)
        {
            urls.Add($"{_BaseRoute}{route}{queryParameters}&{_IncludingDetails}=true");
        }
        else
        {
            urls.Add($"{_BaseRoute}{route}{queryParameters}");
        }

        return urls;
    }

    #endregion
}