using System.ComponentModel.DataAnnotations;

using FluentAssertions;

using HomagConnect.Base.Contracts;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
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
    [TestMethod]
    public async Task BoardsCreateBoardType()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "EB_White_1mm";
        await CreateEdgebandTypeSamples.Edgebands_CreateEdgebandType(materialManagerClient.Material.Edgebands, edgebandCode);
    }

    [ClassCleanup]
    public async Task Cleanup()
    {
        var materialManagerClient = GetMaterialManagerClient();
        await materialManagerClient.Material.Edgebands.DeleteEdgebandType("EB_White_1mm");
        await materialManagerClient.Material.Edgebands.DeleteEdgebandType("EB_White_1mm_AdditionalData");
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

        await act.Should().ThrowAsync<ValidationException>();
    }

    [TestMethod]
    public async Task EdgebandTypeCreation_WithAdditionalData_Succeeds()
    {
        var materialManagerClient = GetMaterialManagerClient();
        var edgebandCode = "EB_White_1mm_AdditionalData";

        await CreateEdgebandTypeSamples.Edgebands_CreateEdgebandType_AdditionalData(
            materialManagerClient.Material.Edgebands, edgebandCode);
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