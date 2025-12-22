using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;
using System.IO;

using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Temp
{
    /// <summary>
    /// Tests for importing Roomle material definitions and mapping to Texture.
    /// </summary>
    [TestClass]
    [TestCategory("MaterialManager")]
    [TestCategory("MaterialManager.Textures")]
    public class TextureTests : TestBase
    {
        /// <summary />
        public TestContext TestContext { get; set; }

        /// <summary />
        [TestMethod]
        public async Task Texture_ImportOrUpdate_MapsAndTraces()
        {
            // Arrange
            var blobStorageConnectionString = GetConfigurationSetting("HomagConnect:BlobStorageConnectionString");
            var materialManagerClientTextures = new MaterialManagerClientTextures(blobStorageConnectionString, SubscriptionId);

            const string inputPath = "Surfaces/Temp/f187_st9.rml";
            var file = new FileInfo(inputPath);
            Assert.IsTrue(file.Exists, $"Input file '{inputPath}' must exist for this test.");

            var materialDefinitionRoomle = MaterialDefinitionRoomle.Deserialize(file);
            Assert.IsNotNull(materialDefinitionRoomle, "Deserialized Roomle material should not be null");
            Assert.IsNotNull(materialDefinitionRoomle!.Material, "Roomle material payload should be present");

            // Act
            var texture = await materialManagerClientTextures.ImportOrUpdate(materialDefinitionRoomle);

            // Assert basic mapping invariants
            Assert.IsFalse(string.IsNullOrWhiteSpace(texture.Id), "Texture Id should be composed or fallback to external id");
            Assert.IsFalse(string.IsNullOrWhiteSpace(texture.Name), "Texture Name should be taken from material label");

            // Output trace for inspection
            texture.Trace();
            TestContext.AddResultFile(file.FullName);
            TestContext.AddResultFile(texture.TraceToFile(texture.Name).FullName);
            TestContext.AddResultFile((await materialManagerClientTextures.GetTextures(int.MaxValue) ?? Array.Empty<Texture>()).TraceToFile("Textures").FullName);
        }
    }
}
