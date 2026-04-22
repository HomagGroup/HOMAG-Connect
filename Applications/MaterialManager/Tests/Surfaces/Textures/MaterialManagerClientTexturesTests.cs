using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures;

/// <summary>
/// Integration and argument-validation tests for <see cref="MaterialManagerClientTextures" />.
/// </summary>
[TestClass]
//[IntegrationTest("MaterialManager.Textures", TestPriority.Medium)]
//[Ignore("These tests needs to be updated.")]
public class MaterialManagerClientTexturesTests : MaterialManagerTestBase
{
    // We accept that the ":" is replaced by "_" in the returned texture id, as long as the catalog and decor code are correct
    private const string TestTextureId = "room_designer_oak";
    private const string TestCatalog = "room_designer";
    private const string TestDecorCode = "oak";
    private const string RmlFilePath = "Surfaces/Textures/room_designer_oak.rml";

    #region Integration tests

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Textures")]
    public async Task GetTextures_NoFilter_ReturnsResults()
    {
        var client = GetMaterialManagerClient().Textures;
        await EnsureTestTextureExists(client);

        var textures = await client.GetTextures(take: 10);

        textures.ShouldNotBeNull("because GetTextures should return a non-null result");
    }

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Textures")]
    public async Task GetTextures_ByCatalog_ReturnsResults()
    {
        var client = GetMaterialManagerClient().Textures;
        await EnsureTestTextureExists(client);

        var textures = await client.GetTextures(catalog: TestCatalog, take: 10);

        textures.ShouldNotBeNull($"because GetTextures filtered by catalog '{TestCatalog}' should return a non-null result");
    }

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Textures")]
    public async Task GetTextures_ByCatalogAndDecorCode_ReturnsResults()
    {
        var client = GetMaterialManagerClient().Textures;
        await EnsureTestTextureExists(client);

        var textures = await client.GetTextures(catalog: TestCatalog, decorCode: TestDecorCode, take: 10);

        textures.ShouldNotBeNull($"because GetTextures filtered by catalog '{TestCatalog}' and decor code '{TestDecorCode}' should return a non-null result");
    }

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Textures")]
    public async Task GetTexture_ById_ReturnsExpectedTexture()
    {
        var client = GetMaterialManagerClient().Textures;
        await EnsureTestTextureExists(client);

        var texture = await client.GetTexture(TestTextureId);

        texture.ShouldNotBeNull($"because texture with id '{TestTextureId}' should exist after import");
        texture.Id.ShouldBe(TestTextureId, "because the returned texture id should match the requested id");
    }

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Textures")]
    public async Task ImportOrUpdate_SingleMaterial_ReturnsTexture()
    {
        var client = GetMaterialManagerClient().Textures;
        var material = await LoadTestMaterial();

        var texture = await client.ImportOrUpdate(material);

        texture.ShouldNotBeNull("because ImportOrUpdate should return the resulting texture");
        texture.Id.ShouldBe(TestTextureId, "because the returned texture id should match the imported material id");
    }

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Textures")]
    public async Task ImportOrUpdate_BatchMaterials_ReturnsTextures()
    {
        var client = GetMaterialManagerClient().Textures;
        var material = await LoadTestMaterial();
        var batch = new MaterialDefinitionsRoomle
        {
            Materials = [material.Material!]
        };

        var textures = await client.ImportOrUpdate(batch);

        textures.ShouldNotBeNull("because batch ImportOrUpdate should return resulting textures");
        textures.Length.ShouldBeGreaterThan(0, "because at least one texture should be returned");
    }

    /// <summary />
    [TestMethod]
    [IntegrationTest("MaterialManager.Textures")]
    public async Task DeleteTexture_ExistingTexture_DoesNotThrow()
    {
        var client = GetMaterialManagerClient().Textures;
        await EnsureTestTextureExists(client);

        var act = async () => await client.DeleteTexture(TestTextureId);

        await Should.NotThrowAsync(act, $"because deleting existing texture '{TestTextureId}' should not throw");
    }

    #endregion

    #region Argument validation tests

    /// <summary />
    [TestMethod]
    public async Task GetTexture_WithNullId_ThrowsArgumentNullException()
    {
        var client = new MaterialManagerClientTextures(new HttpClient());
        await Should.ThrowAsync<ArgumentNullException>(() => client.GetTexture(null!));
    }

    /// <summary />
    [TestMethod]
    public async Task GetTexture_WithEmptyId_ThrowsArgumentNullException()
    {
        var client = new MaterialManagerClientTextures(new HttpClient());
        await Should.ThrowAsync<ArgumentNullException>(() => client.GetTexture(string.Empty));
    }

    /// <summary />
    [TestMethod]
    public async Task GetTexture_WithWhiteSpaceId_ThrowsArgumentNullException()
    {
        var client = new MaterialManagerClientTextures(new HttpClient());
        await Should.ThrowAsync<ArgumentNullException>(() => client.GetTexture("   "));
    }

    /// <summary />
    [TestMethod]
    public async Task ImportOrUpdate_WithNullSingleMaterial_ThrowsArgumentNullException()
    {
        var client = new MaterialManagerClientTextures(new HttpClient());
        await Should.ThrowAsync<ArgumentNullException>(() => client.ImportOrUpdate((MaterialDefinitionRoomle)null!));
    }

    /// <summary />
    [TestMethod]
    public async Task ImportOrUpdate_WithNullBatch_ThrowsArgumentNullException()
    {
        var client = new MaterialManagerClientTextures(new HttpClient());
        await Should.ThrowAsync<ArgumentNullException>(() => client.ImportOrUpdate((MaterialDefinitionsRoomle)null!));
    }

    /// <summary />
    [TestMethod]
    public async Task DeleteTexture_WithNullId_ThrowsArgumentNullException()
    {
        var client = new MaterialManagerClientTextures(new HttpClient());
        await Should.ThrowAsync<ArgumentNullException>(() => client.DeleteTexture(null!));
    }

    /// <summary />
    [TestMethod]
    public async Task DeleteTexture_WithEmptyId_ThrowsArgumentNullException()
    {
        var client = new MaterialManagerClientTextures(new HttpClient());
        await Should.ThrowAsync<ArgumentNullException>(() => client.DeleteTexture(string.Empty));
    }

    #endregion

    #region Cleanup

    /// <summary />
    [ClassCleanup]
    public static async Task Cleanup()
    {
        var instance = new MaterialManagerClientTexturesTests();
        try
        {
            await instance.GetMaterialManagerClient().Textures.DeleteTexture(TestTextureId);
        }
        catch (Exception)
        {
            // ignored - texture may have been deleted by a test or may not exist
        }
    }

    #endregion

    #region Helpers

    private static async Task EnsureTestTextureExists(MaterialManagerClientTextures client)
    {
        var material = await LoadTestMaterial();
        var texture = await client.ImportOrUpdate(material);
        Assert.IsNotNull(texture, "Test texture should be created successfully for integration tests");
    }

    private static async Task<MaterialDefinitionRoomle> LoadTestMaterial()
    {
        Assert.IsTrue(File.Exists(RmlFilePath), $"Test RML file '{RmlFilePath}' must exist.");
        var json = await File.ReadAllTextAsync(RmlFilePath);
        var material = MaterialDefinitionRoomle.FromJson(json);
        Assert.IsNotNull(material, "Test material must not be null.");
        return material!;
    }

    #endregion
}
