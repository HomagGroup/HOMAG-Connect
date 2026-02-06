using System.ComponentModel.DataAnnotations;

using Shouldly;

using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Edgebands;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace HomagConnect.MaterialManager.Tests.Create.Edgebands;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class CreateEdgebandTypeTests : MaterialManagerTestBase
{
    private const string _EdgebandCodeWhite = "ABS_White_3mm";
    private const string _EdgebandCodeAdditionalData = "EB_White_1mm_AdditionalData";

    private MaterialManagerClientMaterialEdgebands _MaterialManagerClientMaterialEdgebands;

    /// <summary />
    [TestCleanup]
    public async Task Cleanup()
    {
        await CleanupEdgebands();
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsCreateEdgebandType()
    {
        var act = async () => await CreateEdgebandTypeSamples.Edgebands_CreateEdgebandType(_MaterialManagerClientMaterialEdgebands, _EdgebandCodeWhite);

        await Should.NotThrowAsync(act,
            $"because creating edgeband type with edgeband code '{_EdgebandCodeWhite}' should complete successfully");
    }

    /// <summary />
    [TestMethod]
    [DataRow(null, 20, 1.2, 150)] // EdgebandCode not set
    [DataRow("EdgebandCode", null, 1.2, 150)] // Height not set
    [DataRow("EdgebandCode", 20, null, 150)] // Thickness not set
    [DataRow("EdgebandCode", 20, 1.2, null)] // Length not set
    public async Task EdgebandTypeCreation_RequiredPropertiesMissing_ThrowsException(string edgebandCode, double height, double thickness, double length)
    {
        var requestEdgebandType = CreateEdgebandTypeRequest(edgebandCode, height, thickness, length);

        var act = async () => await _MaterialManagerClientMaterialEdgebands.CreateEdgebandType(requestEdgebandType);

        await Should.ThrowAsync<ValidationException>(act,
            "because creating an edgeband type with missing required properties should throw a ValidationException");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandTypeCreation_WithAdditionalData_Succeeds()
    {
        var act = async () => await CreateEdgebandTypeSamples.Edgebands_CreateEdgebandType_AdditionalData(
            _MaterialManagerClientMaterialEdgebands, _EdgebandCodeAdditionalData);

        await Should.NotThrowAsync(act,
            $"because creating edgeband type with edgeband code '{_EdgebandCodeAdditionalData}' and additional data should complete successfully");
    }

    /// <summary>
    /// </summary>
    [TestInitialize]
    public async Task Initialize()
    {
        _MaterialManagerClientMaterialEdgebands = GetMaterialManagerClient().Material.Edgebands;
        await CleanupEdgebands();
    }

    private async Task CleanupEdgebands()
    {
        await DeleteEdgeBand(_EdgebandCodeWhite);
        await DeleteEdgeBand(_EdgebandCodeAdditionalData);
    }

    private static MaterialManagerRequestEdgebandType CreateEdgebandTypeRequest(string edgebandCode, double height, double thickness, double length)
    {
        var edgebandType = new MaterialManagerRequestEdgebandType();
        edgebandType.EdgebandCode = edgebandCode;
        edgebandType.Height = height;
        edgebandType.Thickness = thickness;
        edgebandType.DefaultLength = length;
        return edgebandType;
    }

    private async Task DeleteEdgeBand(string edgebandCode)
    {
        try

        {
            await _MaterialManagerClientMaterialEdgebands.DeleteEdgebandType(edgebandCode);
        }

        catch (Exception)

        {
            // ignored if the board entity does not exist
        }
    }
}