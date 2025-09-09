using System.Net.Http.Headers;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
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

    protected async Task BoardsDeleteTestData(string boardCode)
    {
        var materialManagerClient = GetMaterialManagerClient();
        try
        {
            await materialManagerClient.Material.Boards.DeleteBoardType(boardCode);
        }
        catch 
        {
            
        }
    }

    protected async Task EnsureSampleMaterialCodeExist(string materialCode)
    {
        var materialManagerClient = GetMaterialManagerClient();

        try
        {
            await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCode(materialCode);
        }
        catch (ProblemDetailsException ex)
        {
            if (ex.Message.Contains("No board types found."))
            {
                await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
                {
                    MaterialCode = materialCode,
                    BoardCode = "HPL_F274_9_12.0_2070_2800",
                    Thickness = 19.0,
                    Width = 2070,
                    Length = 2800,
                    Type = BoardTypeType.Board,
                    CoatingCategory = CoatingCategory.Undefined,
                    MaterialCategory = BoardMaterialCategory.Undefined,
                    Grain = Base.Contracts.Enumerations.Grain.None,
                });
            }
            else
            {
                throw;
            }
        }
    }
}