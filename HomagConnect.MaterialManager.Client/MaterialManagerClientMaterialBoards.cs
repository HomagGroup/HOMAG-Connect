using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using HomagConnect.Base.Services;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialManager.Client;

public class MaterialManagerClientMaterialBoards : ServiceBase, IMaterialManagerClientMaterialBoards
{
    public const string BaseRoute = "api/materialManager/materials/boards";
    public const string InventoryRoute = "/inventory";
    public const string MaterialCodesRoute = "/materialCodes";
    public const string MaterialCode = "materialCode";
    public const string BoardCode = "boardCode";
    public const string IncludingDetails = "includingDetails";

    public MaterialManagerClientMaterialBoards(HttpClient client) : base(client) { }

    public async Task<BoardType> GetBoardType(string boardCode)
    {
        var url = $"{BaseRoute}?{BoardCode}={Uri.EscapeDataString(boardCode)}";

        return await RequestObject<BoardType>(url);
    }

    public async Task<IReadOnlyCollection<BoardCodeWithInventory>> GetBoardTypeInventory(IEnumerable<string> boardCodes)
    {
        var boardCodeList = boardCodes.ToList();
        if (!boardCodeList.Any())
        {
            throw new ArgumentNullException(nameof(boardCodes), "At least one board code must be passed.");
        }

        var codes = boardCodeList.OrderBy(b => b).Distinct().Where(b => !string.IsNullOrWhiteSpace(b));

        var urls = CreateUrls(codes, BoardCode, InventoryRoute);

        var boardCodesWithInventory = new List<BoardCodeWithInventory>();
        foreach (var url in urls)
        {
            boardCodesWithInventory.AddRange(await RequestEnumerable<BoardCodeWithInventory>(url));
        }

        return boardCodesWithInventory;
    }

    public async Task<IReadOnlyCollection<BoardType>> GetBoardTypes(int take, int skip = 0)
    {
        var url = $"{BaseRoute}?take={take}&skip={skip}";

        return (await RequestEnumerable<BoardType>(url)).ToList();
    }

    public async Task<IReadOnlyCollection<BoardType>> GetBoardTypes(IEnumerable<string> boardCodes)
    {
        var boardCodeList = boardCodes.ToList();
        if (!boardCodeList.Any())
        {
            throw new ArgumentNullException(nameof(boardCodes), "At least one board code must be passed.");
        }

        var codes = boardCodeList.OrderBy(b => b).Distinct().Where(b => !string.IsNullOrWhiteSpace(b));

        var urls = CreateUrls(codes, BoardCode);

        var boardTypes = new List<BoardType>();
        foreach (var url in urls)
        {
            boardTypes.AddRange(await RequestEnumerable<BoardType>(url));
        }

        return boardTypes;
    }

    public async Task<IReadOnlyCollection<BoardType>> GetBoardTypesByMaterialCode(string materialCode)
    {
        var url = $"{BaseRoute}?{MaterialCode}={Uri.EscapeDataString(materialCode)}";

        return (await RequestEnumerable<BoardType>(url)).ToList();
    }

    public async Task<IReadOnlyCollection<BoardTypeDetails>> GetBoardTypesByMaterialCodeIncludingDetails(string materialCode)
    {
        var url = $"{BaseRoute}?{MaterialCode}={Uri.EscapeDataString(materialCode)}&{IncludingDetails}=true";

        return (await RequestEnumerable<BoardTypeDetails>(url)).ToList();
    }

    public async Task<IReadOnlyCollection<BoardType>> GetBoardTypesByMaterialCodes(IEnumerable<string> materialCodes)
    {
        var materialCodeList = materialCodes as string[] ?? materialCodes.ToArray();
        if (!materialCodeList.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        var codes = materialCodeList.OrderBy(b => b).Distinct().Where(b => !string.IsNullOrWhiteSpace(b));

        var urls = CreateUrls(codes, MaterialCode);

        var boardCodesWithInventory = new List<BoardType>();
        foreach (var url in urls)
        {
            boardCodesWithInventory.AddRange(await RequestEnumerable<BoardType>(url));
        }

        return boardCodesWithInventory;
    }

    public async Task<IReadOnlyCollection<BoardTypeDetails>> GetBoardTypesByMaterialCodesIncludingDetails(IEnumerable<string> materialCodes)
    {
        var materialCodeList = materialCodes.ToList();
        if (!materialCodeList.Any())
        {
            throw new ArgumentNullException(nameof(materialCodes), "At least one material code must be passed.");
        }

        var codes = materialCodeList.OrderBy(b => b).Distinct().Where(b => !string.IsNullOrWhiteSpace(b));

        var uris = CreateUrls(codes, MaterialCode, includingDetails: true);

        var boardTypesDetails = new List<BoardTypeDetails>();
        foreach (var uri in uris)
        {
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(uri));
        }

        return boardTypesDetails;
    }

    public async Task<IReadOnlyCollection<BoardTypeDetails>> GetBoardTypesIncludingDetails(IEnumerable<string> boardCodes)
    {
        var boardCodeList = boardCodes.ToList();
        if (!boardCodeList.Any())
        {
            throw new ArgumentNullException(nameof(boardCodes), "At least one board code must be passed.");
        }

        var codes = boardCodeList.OrderBy(b => b).Distinct().Where(b => !string.IsNullOrWhiteSpace(b));

        var urls = CreateUrls(codes, BoardCode, includingDetails: true);

        var boardTypesDetails = new List<BoardTypeDetails>();
        foreach (var url in urls)
        {
            boardTypesDetails.AddRange(await RequestEnumerable<BoardTypeDetails>(url));
        }

        return boardTypesDetails;
    }

    public async Task<IReadOnlyCollection<MaterialCodeWithThumbnail>> GetMaterialCodes(string search, int take, int skip = 0)
    {
        var url = $"{BaseRoute}{MaterialCodesRoute}?search={Uri.EscapeDataString(search)}&take={take}&skip={skip}";

        return (await RequestEnumerable<MaterialCodeWithThumbnail>(url)).ToList();
    }

    public async Task<IReadOnlyCollection<MaterialCodeWithThumbnail>> GetMaterialCodes(int take, int skip = 0)
    {
        var url = $"{BaseRoute}{MaterialCodesRoute}?take={take}&skip={skip}";

        return (await RequestEnumerable<MaterialCodeWithThumbnail>(url)).ToList();
    }

    private List<string> CreateUrls(IEnumerable<string> codes, string searchCode, string route = "", bool includingDetails = false)
    {
        var queryParameters = new StringBuilder("?");
        var i = 1;
        var urls = new List<string>();
        var codeList = codes.ToList();
        while (i <= codeList.Count)
        {
            queryParameters.Append($"{searchCode}={Uri.EscapeDataString(codeList[i - 1])}");
            // To reduce the size of the URL, we are going to split the request into multiple requests. Max URL length is 2048, that´s why we are using 1900 as the limit.
            if (queryParameters.Length + BaseRoute.Length > 1900)
            {
                if (includingDetails)
                {
                    urls.Add($"{BaseRoute}{route}{queryParameters}&{includingDetails}=true");
                }
                else
                {
                    urls.Add($"{BaseRoute}{route}{queryParameters}");
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
            urls.Add($"{BaseRoute}{route}{queryParameters}&{includingDetails}=true");
        }
        else
        {
            urls.Add($"{BaseRoute}{route}{queryParameters}");
        }

        return urls;
    }
}