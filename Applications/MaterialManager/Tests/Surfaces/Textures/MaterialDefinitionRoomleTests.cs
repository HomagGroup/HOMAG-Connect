using Shouldly;

using HomagConnect.Base;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

using Newtonsoft.Json;

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
        const string inputPath = "Surfaces/Textures/u999_st7.rml";
        const string outputPath = "Surfaces/Textures/u999_st7.json";

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
}