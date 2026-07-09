using Shouldly;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

using Newtonsoft.Json;
using HomagConnect.Base.Contracts;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures;

// NOTE: This is preliminary code and is subject to change

/// <summary />
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Textures")]
public class MaterialDefinitionRoomleTests
{
    /// <summary />
    public TestContext TestContext { get; set; }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public async Task RoomleMaterial_RoundtripSerialization_ProducesEquivalentObject()
    {
        // Arrange
        const string inputPath = "Surfaces/Textures/room_designer_oak.rml";
        const string outputPath = "Surfaces/Textures/room_designer_oak.json";

        Assert.IsTrue(File.Exists(inputPath), $"Input file '{inputPath}' must exist for this test.");

        var inputJson = await File.ReadAllTextAsync(inputPath);

        // Act
        var original = JsonConvert.DeserializeObject<MaterialDefinitionRoomle>(inputJson, SerializerSettings.Default);
        Assert.IsNotNull(original, "Deserialized Roomle material should not be null");
        Assert.IsNotNull(original!.Material, "Roomle material payload should be present");

        var serializedJson = JsonConvert.SerializeObject(original, SerializerSettings.Default);
        await File.WriteAllTextAsync(outputPath, serializedJson);

        var roundtripJson = await File.ReadAllTextAsync(outputPath);
        var roundtripped = JsonConvert.DeserializeObject<MaterialDefinitionRoomle>(roundtripJson, SerializerSettings.Default);

        // Assert
        Assert.IsNotNull(roundtripped, "Roundtripped material should deserialize");
        var reSerializedRoundtrip = JsonConvert.SerializeObject(roundtripped, SerializerSettings.Default);
        reSerializedRoundtrip.ShouldBe(serializedJson);

        // Attach artifacts for inspection
        TestContext.AddResultFile(inputPath);
        TestContext.AddResultFile(outputPath);
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public async Task RoomleMaterial_CreateThumbnailFile_CreatesPngFromMaterialColor()
    {
        // Arrange
        const string inputPath = "Surfaces/Textures/u999_st7.rml";

        Assert.IsTrue(File.Exists(inputPath), $"Input file '{inputPath}' must exist for this test.");

        var inputJson = await File.ReadAllTextAsync(inputPath);
        var materialDefinition = JsonConvert.DeserializeObject<MaterialDefinitionRoomle>(inputJson, SerializerSettings.Default);
        Assert.IsNotNull(materialDefinition, "Deserialized Roomle material should not be null");
        Assert.IsNotNull(materialDefinition!.Material, "Roomle material payload should be present");

        materialDefinition.Material!.Thumbnail = null;

        var workingDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"roomle-thumbnail-{Guid.NewGuid():N}"));

        // Act
        var thumbnailFile = await materialDefinition.GetOrCreateThumbnailFileAsync(workingDirectoryInfo: workingDirectory);

        // Assert
        Assert.IsNotNull(thumbnailFile, "Thumbnail file should be created from the material color fallback");
        thumbnailFile!.Exists.ShouldBeTrue("because the thumbnail service should write the PNG file to disk");
        thumbnailFile.Extension.ShouldBe(".png", "because generated thumbnails are PNG files");

        var thumbnailBytes = await File.ReadAllBytesAsync(thumbnailFile.FullName);
        thumbnailBytes.Length.ShouldBeGreaterThan(8, "because a PNG file should contain the PNG signature and image chunks");
        thumbnailBytes.Take(8).ToArray().ShouldBe([137, 80, 78, 71, 13, 10, 26, 10],
            "because generated thumbnail files should start with the PNG signature");

        TestContext.AddResultFile(thumbnailFile.FullName);
    }

    /// <summary />
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public async Task RoomleMaterial_CreateThumbnailFile_DownloadsThumbnail()
    {
        // Arrange
        const string inputPath = "Surfaces/Textures/u999_st7.rml";
        const string thumbnailUrl = "https://core.homag.cloud/cdn/images/illustrations/homag.png";

        Assert.IsTrue(File.Exists(inputPath), $"Input file '{inputPath}' must exist for this test.");

        var inputJson = await File.ReadAllTextAsync(inputPath);
        var materialDefinition = JsonConvert.DeserializeObject<MaterialDefinitionRoomle>(inputJson, SerializerSettings.Default);
        Assert.IsNotNull(materialDefinition, "Deserialized Roomle material should not be null");
        Assert.IsNotNull(materialDefinition!.Material, "Roomle material payload should be present");

        materialDefinition.Material!.Thumbnail = thumbnailUrl;

        using var httpClient = new HttpClient();
        var expectedThumbnailBytes = await httpClient.GetByteArrayAsync(thumbnailUrl);

        var workingDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"roomle-thumbnail-download-{Guid.NewGuid():N}"));

        // Act
        var thumbnailFile = await materialDefinition.GetOrCreateThumbnailFileAsync(httpClient, workingDirectory);

        // Assert
        Assert.IsNotNull(thumbnailFile, "Thumbnail file should be created from the downloaded thumbnail bytes");
        thumbnailFile!.Exists.ShouldBeTrue("because the thumbnail service should write the downloaded file to disk");

        var thumbnailBytes = await File.ReadAllBytesAsync(thumbnailFile.FullName);
        thumbnailBytes.ShouldBe(expectedThumbnailBytes,
            "because the downloaded thumbnail bytes should be written without modification");

        TestContext.AddResultFile(thumbnailFile.FullName);
    }
}