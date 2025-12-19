using System.Globalization;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures;

/// <summary>
/// Tests for texture asset URL building, localization, and tracing.
/// </summary>
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Textures")]
public class TextureTests
{
    /// <summary />
    public TestContext TestContext { get; set; }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public void Texture_BuildsVersionedAssetUris_And_Traces()
    {
        // Arrange
        const string baseUrl = "https://materialmanager.homag.cloud/cdn/textures/{subscriptionId}";
        var texture = new Texture
        {
            Id = "egger:u999_st7",
            Catalog = "egger",
            DecorCode = "U999",
            Embossing = "ST7",
            Name = "EGGER U999 ST7",
            ExternalId = "egger:u999_st7",
            ModifiedAt = new DateTime(2025, 02, 20, 14, 27, 34)
        };

        var versionTag = texture.ModifiedAt.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture);
        var relativePath = $"/{texture.Catalog}/{texture.DecorCode}_{texture.Embossing}_v{versionTag}";

        // Act
        texture.AdditionalData =
        [
            new AdditionalDataImage
            {
                Name = texture.Name,
                Category = "Thumbnail",
                DownloadUri = new Uri($"{baseUrl}{relativePath}.png")
            },
            new AdditionalDataSurfaceTexture
            {
                Name = texture.Name,
                Category = "Roomle",
                DownloadUri = new Uri($"{baseUrl}{relativePath}.rml")
            }
        ];

        texture.Trace();

        // Assert
        var thumbnail = texture.AdditionalData.OfType<AdditionalDataImage>().FirstOrDefault();
        var rml = texture.AdditionalData.OfType<AdditionalDataSurfaceTexture>().FirstOrDefault();

        Assert.IsNotNull(thumbnail, "thumbnail additional data should be present");
        StringAssert.Contains(thumbnail!.DownloadUri!.AbsoluteUri, versionTag, "thumbnail URL should include version tag");
        Assert.IsTrue(thumbnail.DownloadUri!.AbsoluteUri.EndsWith(".png", StringComparison.OrdinalIgnoreCase), "thumbnail should point to a PNG file");

        Assert.IsNotNull(rml, "Roomle surface texture additional data should be present");
        StringAssert.Contains(rml!.DownloadUri!.AbsoluteUri, versionTag, "Roomle URL should include version tag");
        Assert.IsTrue(rml.DownloadUri!.AbsoluteUri.EndsWith(".rml", StringComparison.OrdinalIgnoreCase), "Roomle URL should point to a RML file");

        // Attach trace file for inspection
        TestContext.AddResultFile(texture.TraceToFile(texture.Name).FullName);
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public void Texture_EnumDisplayName_Localized_DE()
    {
        // Act
        var germanDisplayNames = EnumExtensions.GetDisplayNames<AdditionalDataType>(new CultureInfo("de-DE"));

        // Assert
        // ReSharper disable once StringLiteralTypo
        Assert.AreEqual("Oberflächentextur", germanDisplayNames[AdditionalDataType.SurfaceTexture]);
    }
}