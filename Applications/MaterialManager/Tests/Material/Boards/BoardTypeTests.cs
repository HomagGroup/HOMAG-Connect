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
        BaseUrl.Should().NotBeNull();
        SubscriptionId.Should().NotBeEmpty();
        AuthorizationKey.Should().NotBeNullOrEmpty();
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2025, 5, 15, "DF-Material")]
    public async Task BoardType_CreateBoardTypeWithAdditionalDataImage()
    {
        var materialManagerClient = GetMaterialManagerClient();

        const string materialCode = "P2_CreateBoardTypeTest_19.0";
        var additionalDataImage = new FileReference("Red.png", @"Data\Red.png");

        await BoardType_CreateBoardType_Cleanup(materialManagerClient, materialCode);

        var boardType = await materialManagerClient.Material.Boards.CreateBoardType(new MaterialManagerRequestBoardType
        {
            MaterialCode = materialCode,
            BoardCode = $"{materialCode}_2800_2070",
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
            TotalAreaAvailableWarningLimit = 60
        };

        boardTypeMetric.Trace();

        var boardTypeImperial = boardTypeMetric.SwitchUnitSystem(UnitSystem.Imperial, true);

        boardTypeImperial.Trace();

        Assert.AreEqual(UnitSystem.Imperial, boardTypeImperial.UnitSystem);

        Assert.AreNotEqual(boardTypeMetric.Length, boardTypeImperial.Length);
        Assert.AreNotEqual(boardTypeMetric.Width, boardTypeImperial.Width);
        Assert.AreNotEqual(boardTypeMetric.Thickness, boardTypeImperial.Thickness);
        Assert.AreNotEqual(boardTypeMetric.TotalAreaAvailableWarningLimit, boardTypeImperial.TotalAreaAvailableWarningLimit);
    }

    private static async Task BoardType_CreateBoardType_Cleanup(MaterialManagerClient materialManagerClient, string materialCode)
    {
        var existingBoardTypes = await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes([materialCode]);

        foreach (var existingBoardType in existingBoardTypes)
        {
            await materialManagerClient.Material.Boards.DeleteBoardType(existingBoardType.BoardCode);
        }

        existingBoardTypes = await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes([materialCode]);

        Assert.IsFalse(existingBoardTypes.Any());
    }
}