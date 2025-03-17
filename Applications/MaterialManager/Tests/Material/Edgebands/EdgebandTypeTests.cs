using FluentAssertions;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

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
        BaseUrl.Should().NotBeNull();
        SubscriptionId.Should().NotBeEmpty();
        AuthorizationKey.Should().NotBeNullOrEmpty();
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 5, 15, "DF-Material")]
    public async Task EdgebandType_CreateEdgebandTypeWithAdditionalDataImage()
    {
        var materialManagerClient = GetMaterialManagerClient();

        const string edgebandCode = "ABS_CreateEdgebandTypeTest";
        var additionalDataImage = new FileReference("Red.png", @"Data\Red.png");

        await EdgebandType_CreateEdgebandType_Cleanup(materialManagerClient, edgebandCode);

        var edgebandType = await materialManagerClient.Material.Edgebands.CreateEdgebandType(new MaterialManagerRequestEdgebandType
        {
            EdgebandCode = $"{edgebandCode}_150_1",
            Thickness = 1.0,
            Height = 20,
            DefaultLength = 23.0,
            MaterialCategory = EdgebandMaterialCategory.Veneer,
            Process = Process.Other,
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

        edgebandType.Trace();
    }

    /// <summary />
    [TestMethod]
    public void EdgebandType_SwitchUnitSystem_LengthWidthThicknessChanged()
    {
        var edgebandTypeMetric = new EdgebandType()
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

        Assert.AreEqual(UnitSystem.Imperial, edgebandTypeImperial.UnitSystem);

        Assert.AreNotEqual(edgebandTypeMetric.DefaultLength, edgebandTypeImperial.DefaultLength);
        Assert.AreNotEqual(edgebandTypeMetric.Thickness, edgebandTypeImperial.Thickness);
        Assert.AreNotEqual(edgebandTypeMetric.ProtectionFilmThickness, edgebandTypeImperial.ProtectionFilmThickness);
        Assert.AreNotEqual(edgebandTypeMetric.Airtec, edgebandTypeImperial.Airtec);
        Assert.AreNotEqual(edgebandTypeMetric.TotalLengthAvailableWarningLimit, edgebandTypeImperial.TotalLengthAvailableWarningLimit);
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 5, 15, "DF-Material")]
    public async Task EdgebandType_Machines_GetAll_ReturnsData()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var machines = await materialManagerClient.Material.Edgebands.GetLicensedMachines();

        machines.Should().NotBeNullOrEmpty("Machines should be assigned to test subscription.");
    }
    
    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 5, 15, "DF-Material")]
    public async Task EdgebandType_TechnologyMacros_GetByMachine_ReturnsData()
    {
        var materialManagerClient = GetMaterialManagerClient();

        var machines = await materialManagerClient.Material.Edgebands.GetLicensedMachines();
        var macros = await materialManagerClient.Material.Edgebands.GetTechnologyMacrosFromMachine(machines.First().TapioMachineId);
        
        macros.Should().NotBeNull();
    }

    private static async Task EdgebandType_CreateEdgebandType_Cleanup(MaterialManagerClient materialManagerClient, string edgebandCode)
    {
        var existingEdgebandTypes = await materialManagerClient.Material.Edgebands.GetEdgebandTypesByEdgebandCodes([edgebandCode]);

        foreach (var existingEdgebandType in existingEdgebandTypes)
        {
            if (existingEdgebandType.EdgebandCode != null)
            {
                await materialManagerClient.Material.Edgebands.DeleteEdgebandType(existingEdgebandType.EdgebandCode);
            }
        }

        existingEdgebandTypes = await materialManagerClient.Material.Edgebands.GetEdgebandTypesByEdgebandCodes([edgebandCode]);

        Assert.IsFalse(existingEdgebandTypes.Any());
    }
}