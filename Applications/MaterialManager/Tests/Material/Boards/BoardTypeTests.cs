using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;
using HomagConnect.MaterialManager.Contracts.Request;
using Shouldly;

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
        BaseUrl.ShouldNotBeNull(
            "because BaseUrl should be configured for MaterialManager tests");
        SubscriptionId.ShouldNotBe(Guid.Empty,
            "because SubscriptionId should be configured for MaterialManager tests");
        AuthorizationKey.ShouldNotBeNullOrEmpty(
            "because AuthorizationKey should be configured for MaterialManager tests");
    }

    /// <summary />
    [TestMethod]
    [TemporaryDisabledOnServer(2026, 03, 31, "DF-Material")]
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

        boardType.ShouldNotBeNull(
            $"because board type with board code '{fullBoardCode}' should be created successfully");
        boardType!.BoardCode.ShouldBe(fullBoardCode,
            $"because created board type should have board code '{fullBoardCode}'");
        boardType.MaterialCode.ShouldBe(materialCode,
            $"because created board type should have material code '{materialCode}'");

        boardType.Trace();
    }

    private static async Task BoardType_CreateBoardType_Cleanup(MaterialManagerClient materialManagerClient, string materialCode)
    {
        var existingBoardTypes = (await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes([materialCode])).ToArray();

        foreach (var existingBoardType in existingBoardTypes)
        {
            await materialManagerClient.Material.Boards.DeleteBoardType(existingBoardType.BoardCode);
        }

        existingBoardTypes = (await materialManagerClient.Material.Boards.GetBoardTypesByMaterialCodes([materialCode])).ToArray();

        existingBoardTypes.ShouldBeEmpty(
            $"because all board types with material code '{materialCode}' should be deleted during cleanup");
    }

}