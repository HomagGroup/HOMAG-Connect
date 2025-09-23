using System.Net.Http.Headers;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Tests;

/// <summary />
public class MaterialManagerTestBase : TestBase
{
    /// <summary>
    /// Gets a new instance of the <see cref="MaterialManagerClient" />.
    /// </summary>
    protected MaterialManagerClient GetMaterialManagerClient()
    {
        $"BaseUrl: {BaseUrl}, Subscription: {SubscriptionId}, AuthorizationKey: {AuthorizationKey[..4]}*".Trace();

        var httpClient = new HttpClient
        {
            BaseAddress = BaseUrl
        };

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", EncodeBase64Token(SubscriptionId.ToString(), AuthorizationKey));

        return new MaterialManagerClient(httpClient);
    }

    protected async Task EnsureBoardTypeExist(string materialCode, double length = 2800, double width = 2070)
    {
        var boardCode = $"{materialCode}_{length}_{width}";
        var materialManagerClient = GetMaterialManagerClient();

        BoardType? boardType = null;

        try
        {
            boardType = await materialManagerClient.Material.Boards.GetBoardTypeByBoardCode(boardCode);
        }
        catch (ProblemDetailsException ex)
        {
            if (!ex.Message.Contains("No board types found."))
            {
                throw;
            }
        }

        if (boardType == null)
        {
            await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
            {
                MaterialCode = materialCode,
                BoardCode = boardCode,
                Thickness = 19.0,
                Grain = Grain.None,
                Width = width,
                Length = length,
                Type = BoardTypeType.Board,
                CoatingCategory = CoatingCategory.Undefined,
                MaterialCategory = BoardMaterialCategory.Undefined
            });
        }
    }

    protected async Task EnsureEdgebandTypeExist(string edgebandCode, double thickness = 1.0, double length = 23.0)
    {
        var materialManagerClient = GetMaterialManagerClient();

        EdgebandType? edgebandType = null;

        try
        {
            edgebandType = await materialManagerClient.Material.Edgebands.GetEdgebandTypeByEdgebandCode(edgebandCode);
        }
        catch (ProblemDetailsException ex)
        {
            if (!ex.Message.Contains("No edgeband types found."))
            {
                throw;
            }
        }

        if (edgebandType == null)
        {
            await materialManagerClient.Material.Edgebands.CreateEdgebandType(new MaterialManagerRequestEdgebandType
            {
                EdgebandCode = edgebandCode,
                Height = 20,
                Thickness = thickness,
                DefaultLength = length,
                MaterialCategory = EdgebandMaterialCategory.ABS,
                Process = EdgebandingProcess.Other,
            });
        }
    }
}