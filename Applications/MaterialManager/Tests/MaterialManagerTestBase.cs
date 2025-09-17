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

    protected async Task EnsureBoardTypeExist(string materialCode)
    {
        var materialManagerClient = GetMaterialManagerClient();

        IList<BoardType> boardTypes;

        try
        {
            boardTypes = await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes(new[] { materialCode })
                .ToListAsync();
        }
        catch (ProblemDetailsException ex)
        {
            if (ex.Message.Contains("No board types found."))
            {
                boardTypes = new List<BoardType>();
            }
            else
            {
                throw;
            }
        }

        if (boardTypes.All(b => b.MaterialCode != materialCode))
        {
            await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
            {
                MaterialCode = materialCode,
                BoardCode = $"{materialCode}_2800_2070",
                Thickness = 19.0,
                Grain = Grain.None,
                Width = 2070,
                Length = 2800,
                Type = BoardTypeType.Board,
                CoatingCategory = CoatingCategory.Undefined,
                MaterialCategory = BoardMaterialCategory.Undefined
            });
        }
    }

    protected async Task EnsureEdgebandTypeExist(string edgebandCode, double thickness = 1.0, double length = 23.0)
    {
        var materialManagerClient = GetMaterialManagerClient();

        IList<EdgebandType> endgebandTypes;

        try
        {
            endgebandTypes = await materialManagerClient.Material.Edgebands.GetEdgebandTypesByEdgebandCodes(new[] { edgebandCode })
                .ToListAsync();
        }
        catch (ProblemDetailsException ex)
        {
            if (ex.Message.Contains("No edgeband types found."))
            {
                endgebandTypes = new List<EdgebandType>();
            }
            else
            {
                throw;
            }
        }

        if (endgebandTypes.All(b => b.EdgebandCode != edgebandCode))
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