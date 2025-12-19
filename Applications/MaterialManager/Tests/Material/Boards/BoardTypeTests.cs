using FluentAssertions;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;

namespace HomagConnect.MaterialManager.Tests.Material.Boards;

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Boards")]
public class BoardTypeTests : MaterialManagerTestBase
{
    /// <summary />
    [TestMethod]
    public void BoardType_CheckConfiguration_ConfigValid()
    {
        BaseUrl.Should().NotBeNull(
            "because BaseUrl should be configured for MaterialManager tests");
        SubscriptionId.Should().NotBeEmpty(
            "because SubscriptionId should be configured for MaterialManager tests");
        AuthorizationKey.Should().NotBeNullOrEmpty(
            "because AuthorizationKey should be configured for MaterialManager tests");
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Boards")]
    public void BoardType_Density_ReturnsSpecificWhenSet()
    {
        // Arrange
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.Chipboard,
            Density = 485.9
        };

        // Assert
        boardType.DensityOrCategoryTypical.Should().Be(485.9, "because specific density is set");
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Boards")]
    public void BoardType_Density_ReturnsCategoryTypicalWhenSpecificMissing()
    {
        // Arrange
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.Chipboard,
            Density = null
        };

        // Assert
        boardType.DensityOrCategoryTypical.Should().Be(650, "because typical density should be returned when specific density is not set");
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Boards")]
    public void BoardType_Density_ReturnsNullWhenNoCategoryTypical()
    {
        // Arrange
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.Undefined,
            Density = null
        };

        // Assert
        boardType.DensityOrCategoryTypical.Should().BeNull("because Undefined category does not have a typical density");
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Boards")]
    public void BoardType_Density_ConvertsOnUnitSwitch()
    {
        // Arrange
        var boardType = new BoardType
        {
            MaterialCategory = BoardMaterialCategory.MediumdensityFiberboard_MDF,
            Density = null
        };

        // Assert metric typical first
        boardType.DensityOrCategoryTypical.Should().Be(700, "because MDF typical density should be used in metric");

        // Act
        var boardTypeImperial = boardType.SwitchUnitSystem(UnitSystem.Imperial, true);

        // Assert converted value is in a reasonable lb/ft³ range (~43.7)
        boardTypeImperial.DensityOrCategoryTypical.Should().BeGreaterThan(40).And.BeLessThan(50,
            "because MDF typical density should be converted to imperial units");
    }

    /// <summary />
    [TestMethod]
    public async Task BoardType_CreateBoardTypeWithAdditionalDataImage()
    {
        var materialManagerClient = GetMaterialManagerClient();

        const string materialCode = "P2_CreateBoardTypeTest_19.0";
        var additionalDataImage = new FileReference("Red.png", @"Data\Red.png");

        await BoardType_CreateBoardType_Cleanup(materialManagerClient, materialCode);

        var uniqueBoardCode = $"{materialCode}_{Guid.NewGuid().ToString("N")[..8]}";
        var fullBoardCode = $"{uniqueBoardCode}_2800_2070";

        var boardType = await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
        {
            MaterialCode = materialCode,
            BoardCode = fullBoardCode,
            Thickness = 19.0,
            Grain = Grain.Lengthwise,
            Width = 2070,
            Length = 2800,
            Type = BoardTypeType.Board,
            CoatingCategory = CoatingCategory.Undefined,
            MaterialCategory = BoardMaterialCategory.Undefined,
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

        boardType.Should().NotBeNull(
            $"because board type with board code '{fullBoardCode}' should be created successfully");
        boardType.BoardCode.Should().Be(fullBoardCode,
            $"because created board type should have board code '{fullBoardCode}'");
        boardType.MaterialCode.Should().Be(materialCode,
            $"because created board type should have material code '{materialCode}'");

        boardType.Trace();
    }

    /// <summary />
    [TestMethod]
    public void BoardType_SwitchUnitSystem_LengthWidthThicknessChanged()
    {
        var boardTypeMetric = new BoardType
        {
            Length = 2800,
            Width = 2070,
            Thickness = 19,
            TotalAreaAvailableWarningLimit = 60,
            Density = 420
        };

        boardTypeMetric.Trace();

        var boardTypeImperial = boardTypeMetric.SwitchUnitSystem(UnitSystem.Imperial, true);

        boardTypeImperial.Trace();

        boardTypeImperial.UnitSystem.Should().Be(UnitSystem.Imperial,
            "because board type was switched to Imperial unit system");

        boardTypeImperial.Length.Should().NotBe(boardTypeMetric.Length,
            "because length should be converted from metric to imperial units");
        boardTypeImperial.Width.Should().NotBe(boardTypeMetric.Width,
            "because width should be converted from metric to imperial units");
        boardTypeImperial.Thickness.Should().NotBe(boardTypeMetric.Thickness,
            "because thickness should be converted from metric to imperial units");
        boardTypeImperial.TotalAreaAvailableWarningLimit.Should().NotBe(boardTypeMetric.TotalAreaAvailableWarningLimit,
            "because total area warning limit should be converted from metric to imperial units");
        boardTypeImperial.Density.Should().NotBe(boardTypeMetric.Density,
            "because length should be converted from metric to imperial units");
    }

    private static async Task BoardType_CreateBoardType_Cleanup(MaterialManagerClient materialManagerClient, string materialCode)
    {
        var existingBoardTypes = (await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes([materialCode])).ToArray();

        foreach (var existingBoardType in existingBoardTypes)
        {
            await materialManagerClient.Material.Boards.DeleteBoardType(existingBoardType.BoardCode);
        }

        existingBoardTypes = (await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes([materialCode])).ToArray();

        existingBoardTypes.Should().BeEmpty(
            $"because all board types with material code '{materialCode}' should be deleted during cleanup");
    }
}