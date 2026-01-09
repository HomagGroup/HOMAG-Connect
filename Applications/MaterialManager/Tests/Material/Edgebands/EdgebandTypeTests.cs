using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using HomagConnect.MaterialManager.Contracts.Update;
using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Material.Edgebands;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Edgebands")]
public class EdgebandTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public void EdgebandType_CheckConfiguration_ConfigValid()
    {
        BaseUrl.ShouldNotBeNull(
            "because BaseUrl should be configured for MaterialManager tests");
        SubscriptionId.ShouldNotBe(Guid.Empty,
            "because SubscriptionId should be configured for MaterialManager tests");
        AuthorizationKey.ShouldNotBeNullOrEmpty(
            "because AuthorizationKey should be configured for MaterialManager tests");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandType_CreateEdgebandTypeWithAdditionalDataImage()
    {
        var materialManagerClient = GetMaterialManagerClient();

        const string edgebandCode = "ABS_CreateEdgebandTypeTest";
        var additionalDataImage = new FileReference("Red.png", @"Data\Red.png");

        await EdgebandType_CreateEdgebandType_Cleanup(materialManagerClient, edgebandCode);

        var uniqueEdgebandCode = $"{edgebandCode}_{Guid.NewGuid().ToString("N")[..8]}";
        var fullEdgebandCode = $"{uniqueEdgebandCode}_150_1";

        var edgebandType = await materialManagerClient.Material.Edgebands.CreateEdgebandType(new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = fullEdgebandCode,
            Thickness = 1.0,
            Height = 20,
            DefaultLength = 23.0,
            MaterialCategory = EdgebandMaterialCategory.Veneer,
            Process = EdgebandingProcess.Other,
            AdditionalData = new List<AdditionalDataEntity>
            {
                new AdditionalDataImage
                {
                    Category = "Decor",
                    DownloadFileName = additionalDataImage.Reference,
                    DownloadUri = new Uri(additionalDataImage.Reference, UriKind.Relative)
                }
            }
        }, [additionalDataImage]);

        edgebandType.ShouldNotBeNull(
            $"because edgeband type with edgeband code '{fullEdgebandCode}' should be created successfully");
        edgebandType.EdgebandCode.ShouldBe(fullEdgebandCode,
            $"because created edgeband type should have edgeband code '{fullEdgebandCode}'");

        edgebandType.Thickness.ShouldNotBeNull();
        edgebandType.Thickness.Value.ShouldBe(1.0,0.0001, "because created edgeband type should have thickness 1.0");
        
        edgebandType.Height.ShouldBe(20,
            "because created edgeband type should have height 20");

        edgebandType.Trace();
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandType_Machines_GetAll_ReturnsData()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var machines = (await materialManagerClient.Material.Edgebands.GetLicensedMachines() ?? []).ToArray();

        machines.ShouldNotBeNull(
            "because machines should be assigned to test subscription");
        machines.ShouldNotBeEmpty(
            "because machines should be assigned to test subscription");
    }

    /// <summary />
    [TestMethod]
    public void EdgebandType_SwitchUnitSystem_LengthWidthThicknessChanged()
    {
        var edgebandTypeMetric = new EdgebandType
        {
            DefaultLength = 150,
            Thickness = 19,
            Airtec = 4,
            ProtectionFilmThickness = 0.1,
            TotalLengthAvailableWarningLimit = 1000
        };

        edgebandTypeMetric.Trace();

        var edgebandTypeImperial = edgebandTypeMetric.SwitchUnitSystem(UnitSystem.Imperial, true);

        edgebandTypeImperial.Trace();

        edgebandTypeImperial.UnitSystem.ShouldBe(UnitSystem.Imperial,
            "because edgeband type was switched to Imperial unit system");

        edgebandTypeImperial.DefaultLength.ShouldNotBe(edgebandTypeMetric.DefaultLength,
            "because default length should be converted from metric to imperial units");
        edgebandTypeImperial.Thickness.ShouldNotBe(edgebandTypeMetric.Thickness,
            "because thickness should be converted from metric to imperial units");
        edgebandTypeImperial.ProtectionFilmThickness.ShouldNotBe(edgebandTypeMetric.ProtectionFilmThickness,
            "because protection film thickness should be converted from metric to imperial units");
        edgebandTypeImperial.Airtec.ShouldNotBe(edgebandTypeMetric.Airtec,
            "because airtec should be converted from metric to imperial units");
        edgebandTypeImperial.TotalLengthAvailableWarningLimit.ShouldNotBe(edgebandTypeMetric.TotalLengthAvailableWarningLimit,
            "because total length warning limit should be converted from metric to imperial units");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandType_TechnologyMacros_GetByMachine_ReturnsData()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var machines = (await materialManagerClient.Material.Edgebands.GetLicensedMachines() ?? []).ToArray();

        machines.ShouldNotBeNull(
            "because at least one licensed machine should be available to retrieve technology macros");
        machines.ShouldNotBeEmpty(
            "because at least one licensed machine should be available to retrieve technology macros");

        var firstMachine = machines.First();
        var macros = await materialManagerClient.Material.Edgebands.GetTechnologyMacrosFromMachine(firstMachine.TapioMachineId);

        macros.ShouldNotBeNull(
            $"because technology macros should be retrievable for machine '{firstMachine.TapioMachineId}'");
    }

    /// <summary />
    [TestMethod]
    public async Task EdgebandType_UpdateEdgebandTypeWithAdditionalDataImage()
    {
        var materialManagerClient = GetMaterialManagerClient();

        const string edgebandCode = "ABS_UpdateEdgebandTypeTest";
        var additionalDataImage = new FileReference("Red.png", @"Data\Red.png");

        await EdgebandType_CreateEdgebandType_Cleanup(materialManagerClient, edgebandCode);

        var uniqueEdgebandCode = $"{edgebandCode}_{Guid.NewGuid().ToString("N")[..8]}";
        var fullEdgebandCode = $"{uniqueEdgebandCode}_150_1";

        var edgebandType = await materialManagerClient.Material.Edgebands.CreateEdgebandType(new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = fullEdgebandCode,
            Thickness = 1.0,
            Height = 20,
            DefaultLength = 23.0,
            MaterialCategory = EdgebandMaterialCategory.Veneer,
            Process = EdgebandingProcess.Other
        });

        var updatedEdgeBandType = await materialManagerClient.Material.Edgebands.UpdateEdgebandType(fullEdgebandCode, new MaterialManagerUpdateEdgebandType
        {
            AdditionalData = new List<AdditionalDataEntity>
            {
                new AdditionalDataImage
                {
                    Category = "Decor",
                    DownloadFileName = additionalDataImage.Reference,
                    DownloadUri = new Uri(additionalDataImage.Reference, UriKind.Relative)
                }
            }
        }, [additionalDataImage]);

        updatedEdgeBandType.ShouldNotBeNull(
            $"because edgeband type with edgeband code '{fullEdgebandCode}' should be created successfully");
        updatedEdgeBandType.EdgebandCode.ShouldBe(fullEdgebandCode,
            $"because created edgeband type should have edgeband code '{fullEdgebandCode}'");
        updatedEdgeBandType.Thickness.ShouldBe(1.0,
            "because created edgeband type should have thickness 1.0");
        updatedEdgeBandType.Height.ShouldBe(20,
            "because created edgeband type should have height 20");
        updatedEdgeBandType.Thumbnail.ShouldNotBeNull(
            $"because edgeband type with edgeband code '{fullEdgebandCode}' contain an image");

        edgebandType.Trace();
    }

    private static async Task EdgebandType_CreateEdgebandType_Cleanup(MaterialManagerClient materialManagerClient, string edgebandCode)
    {
        var existingEdgebandTypes = (await materialManagerClient.Material.Edgebands.GetEdgebandTypesByEdgebandCodes([edgebandCode])).ToArray();

        foreach (var existingEdgebandType in existingEdgebandTypes)
        {
            if (existingEdgebandType is { EdgebandCode: not null })
            {
                await materialManagerClient.Material.Edgebands.DeleteEdgebandType(existingEdgebandType.EdgebandCode);
            }
        }

        existingEdgebandTypes = (await materialManagerClient.Material.Edgebands.GetEdgebandTypesByEdgebandCodes([edgebandCode])).ToArray();

        existingEdgebandTypes.ShouldBeEmpty(
            $"because all edgeband types with edgeband code '{edgebandCode}' should be deleted during cleanup");
    }
}