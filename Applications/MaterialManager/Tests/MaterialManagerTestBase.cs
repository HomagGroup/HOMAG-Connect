using System.Net.Http.Headers;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Exceptions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Delete;
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
    /// Edgeband code used for testing.
    /// </summary>
    protected const string EdgebandCode = "ABS_White_2mm";   

    /// <summary>
    /// Create a EdgebandTypeAllocationRequest instance.
    /// </summary>
    /// <param name="edgebandCode"></param>
    /// <param name="comments"></param>
    /// <param name="createdBy"></param>
    /// <param name="source"></param>
    /// <param name="workstation"></param>
    /// <param name="allocatedLength"></param>
    /// <param name="customer"></param>
    /// <param name="order"></param>
    /// <param name="project"></param>
    /// <param name="usedLength"></param>
    /// <returns></returns>
    protected static EdgebandTypeAllocationRequest CreateEdgebandTypeAllocationRequest(string edgebandCode, string comments, string createdBy, string source, string workstation,
        double allocatedLength,
        string customer, string order, string project, double usedLength)
    {
        var edgebandTypeAllocationRequest = new EdgebandTypeAllocationRequest
        {
            EdgebandCode = edgebandCode,
            Comments = comments,
            CreatedBy = createdBy,
            Source = source,
            Workstation = workstation,
            AllocatedLength = allocatedLength,
            Customer = customer,
            Order = order,
            Project = project,
            UsedLength = usedLength
        };

        return edgebandTypeAllocationRequest;
    }

    /// <summary>
    /// Edgeband cleanup after creating an edgeband type allocation.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="edgebandCode"></param>
    /// <param name="customer"></param>
    /// <param name="order"></param>
    /// <param name="project"></param>
    protected async Task EdgebandType_CreateEdgebandTypeAllocation_Cleanup(MaterialManagerClientMaterialEdgebands client, string edgebandCode, string customer, string order, string project)
    {
        try
        {
            await client.DeleteEdgebandTypeAllocation(new EdgebandTypeAllocationDelete
            {
                Customer = customer,
                EdgebandCode = edgebandCode,
                Order = order,
                Project = project
            });
        }
        catch (Exception)
        {
            //ignored
        }
    }

    /// <summary>
    /// Ensures that a board type with the given board type code exists.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="boardTypeCode"></param>
    /// <param name="materialCode"></param>
    /// <param name="length"></param>
    /// <param name="width"></param>
    protected async Task EnsureBoardTypeExist(MaterialManagerClientMaterialBoards client, string boardTypeCode, string materialCode, double length, double width)
    {

        BoardType? boardType = null;
        try
        {
            boardType = await client.GetBoardTypeByBoardCode(boardTypeCode);
        }       
        catch (Exception)
        {
            //ignored           
        }

        if (boardType == null)
        {
            await client.CreateBoardType(new MaterialManagerRequestBoardType
            {
                MaterialCode = materialCode,
                BoardCode = boardTypeCode,
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

    /// <summary>
    /// Ensures that an edgeband type with the given edgeband code exists.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="edgebandCode"></param>
    /// <param name="thickness"></param>
    /// <param name="length"></param>
    protected async Task EnsureEdgebandTypeExist(MaterialManagerClientMaterialEdgebands client, string edgebandCode, double thickness = 1.0, double length = 23.0)
    {
        EdgebandType? edgebandType = null;

        try
        {
            edgebandType = await client.GetEdgebandTypeByEdgebandCode(edgebandCode);
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
            await client.CreateEdgebandType(new MaterialManagerRequestEdgebandType
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
}