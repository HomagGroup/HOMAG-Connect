using System.ComponentModel.DataAnnotations;

using FluentAssertions;

using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Samples.Create.Edgebands;

namespace HomagConnect.MaterialManager.Tests.Create.Edgebands;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class CreateEdgebandTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [ClassCleanup]
    public static async Task Cleanup()
    {
        var classInstance = new CreateEdgebandTypeTests();
        var materialManagerClient = classInstance.GetMaterialManagerClient();
        await materialManagerClient.Material.Edgebands.DeleteEdgebandType("ABS_White_3mm");
        await materialManagerClient.Material.Edgebands.DeleteEdgebandType("EB_White_1mm_AdditionalData");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandsCreateEdgebandType()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "ABS_White_3mm";

        var act = async () => await CreateEdgebandTypeSamples.Edgebands_CreateEdgebandType(materialManagerClient.Material.Edgebands, edgebandCode);

        await act.Should().NotThrowAsync(
            $"because creating edgeband type with edgeband code '{edgebandCode}' should complete successfully");
    }

    /// <summary />
    [TestMethod]
    [DataRow(null, 20, 1.2, 150)] // EdgebandCode not set
    [DataRow("EdgebandCode", null, 1.2, 150)] // Height not set
    [DataRow("EdgebandCode", 20, null, 150)] // Thickness not set
    [DataRow("EdgebandCode", 20, 1.2, null)] // Length not set
    public async Task EdgebandTypeCreation_RequiredPropertiesMissing_ThrowsException(string edgebandCode, double height, double thickness, double length)
    {
        var client = new HttpClient();
        var edgebandsClient = new MaterialManagerClientMaterialEdgebands(client);

        var requestEdgebandType = CreateEdgebandTypeRequest(edgebandCode, height, thickness, length);

        var act = async () => await edgebandsClient.CreateEdgebandType(requestEdgebandType);

        await act.Should().ThrowAsync<ValidationException>(
            "because creating an edgeband type with missing required properties should throw a ValidationException");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandTypeCreation_WithAdditionalData_Succeeds()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "EB_White_1mm_AdditionalData";

        var act = async () => await CreateEdgebandTypeSamples.Edgebands_CreateEdgebandType_AdditionalData(
            materialManagerClient.Material.Edgebands, edgebandCode);

        await act.Should().NotThrowAsync(
            $"because creating edgeband type with edgeband code '{edgebandCode}' and additional data should complete successfully");
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
}