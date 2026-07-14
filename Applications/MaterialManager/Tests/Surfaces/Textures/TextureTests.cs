using System.Diagnostics;
using System.Globalization;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Extensions;
using HomagConnect.Base.TestBase.Attributes;
using HomagConnect.MaterialManager.Client;
using HomagConnect.MaterialManager.Contracts.Extension;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

using Shouldly;

namespace HomagConnect.MaterialManager.Tests.Surfaces.Textures;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Unit and integration tests for texture assets, client operations, and localization.
/// </summary>
[TestClass]
[TestCategory("MaterialManager")]
[TestCategory("MaterialManager.Textures")]
public class TextureTests : MaterialManagerTestBase
{
    private const string _TestCatalog = "HomagConnectTest";
    private const string _TestCatalogDecor1 = "dec-001";
    private const string _TestCatalogEmb1 = "st9";
    private const string _TestCatalogDecor2 = "dec-002";
    private const string _TestCatalogEmb2 = "st7";

    private static readonly DateTimeOffset _FixedTestTimestamp = new(2026, 05, 12, 10, 20, 30, TimeSpan.Zero);

    private MaterialManagerClientTextures? _MaterialManagerClientTextures;

    /// <summary />
    public TestContext TestContext { get; set; } = null!;

    /// <summary>
    /// Initializes the texture client.
    /// </summary>
    [TestInitialize]
    public void Init()
    {
        _MaterialManagerClientTextures = GetMaterialManagerClient().Textures;
    }

    #region Unit Tests - Texture Model

    /// <summary>
    /// Tests that texture asset URIs are correctly built with version tags and traced.
    /// </summary>
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

        thumbnail.ShouldNotBeNull("because thumbnail additional data should be present");
        thumbnail.DownloadUri!.AbsoluteUri.ShouldContain(versionTag, customMessage: "because thumbnail URL should include version tag");
        thumbnail.DownloadUri!.AbsoluteUri.ShouldEndWith(".png", Case.Insensitive, "because thumbnail should point to a PNG file");

        rml.ShouldNotBeNull("because Roomle surface texture additional data should be present");
        rml.DownloadUri!.AbsoluteUri.ShouldContain(versionTag, customMessage: "because Roomle URL should include version tag");
        rml.DownloadUri!.AbsoluteUri.ShouldEndWith(".rml", Case.Insensitive, "because Roomle URL should point to a RML file");

        // Attach trace file for inspection
        TestContext.AddResultFile(texture.TraceToFile(texture.Name).FullName);
    }

    /// <summary>
    /// Tests that additional data type enum display names are correctly localized.
    /// </summary>
    [TestMethod]
    [UnitTest("MaterialManager.Textures")]
    public void Texture_EnumDisplayName_Localized_DE()
    {
        // Act
        var germanDisplayNames = EnumExtensions.GetDisplayNames<AdditionalDataType>(new CultureInfo("de-DE"));

        // Assert
        // ReSharper disable once StringLiteralTypo
        germanDisplayNames[AdditionalDataType.SurfaceTexture].ShouldBe("Oberflächentextur");
    }

    #endregion

    #region Integration Tests - _MaterialManagerClientTextures

    /// <summary>
    /// Tests retrieval of a single texture by ID.
    /// </summary>
    [TestMethod]
    public async Task GetTexture_WithValidId_ReturnsTexture()
    {
        // Arrange
        // Ensure test textures exist
        await EnsureTestTexturesExistAsync();
        var expectedId = ComputeExpectedTextureId(_TestCatalog, _TestCatalogDecor1, _TestCatalogEmb1);

        // Act
        var result = await _MaterialManagerClientTextures!.GetTexture(expectedId);

        // Assert
        result.ShouldNotBeNull("because texture with ID should exist");
        result.Id.ShouldBe(expectedId, "because computed ID should match returned texture ID");
        result.Name.ShouldNotBeNullOrWhiteSpace("because texture should have a name");
        result.Catalog.ShouldBe(_TestCatalog, "because texture should belong to test catalog");
        result.DecorCode.ShouldBe(_TestCatalogDecor1, "because texture should have correct decor code");
        result.Embossing.ShouldBe(_TestCatalogEmb1, "because texture should have correct embossing");
        result.ModifiedAt.ShouldNotBe(default(DateTime), "because texture should have a modified date");
    }

    /// <summary>
    /// Tests that GetTexture throws when ID is null.
    /// </summary>
    [TestMethod]
    public async Task GetTexture_WithNullId_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.GetTexture(null!);
        await Should.ThrowAsync<ArgumentNullException>(act);
    }

    /// <summary>
    /// Tests that GetTexture throws when ID is empty.
    /// </summary>
    [TestMethod]
    public async Task GetTexture_WithEmptyId_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.GetTexture(string.Empty);
        await Should.ThrowAsync<ArgumentNullException>(act);
    }

    /// <summary>
    /// Tests that GetTexture throws when ID is whitespace.
    /// </summary>
    [TestMethod]
    public async Task GetTexture_WithWhitespaceId_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.GetTexture("   ");
        await Should.ThrowAsync<ArgumentNullException>(act);
    }

    /// <summary>
    /// Tests that GetTexture throws when texture ID does not exist.
    /// </summary>
    [TestMethod]
    public async Task GetTexture_WithNonExistentId_ThrowsException()
    {
        // Arrange
        const string nonExistentId = "NONEXISTENT_TEXTURE_ID_THAT_DOES_NOT_EXIST";

        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.GetTexture(nonExistentId);
        await Should.ThrowAsync<HttpRequestException>(act,
            "because texture should not be found for non-existent ID and client throws HttpRequestException on 404");
    }

    /// <summary>
    /// Tests retrieval of textures with default pagination using continuation token.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithDefaults_ReturnsPaginatedResult()
    {
        // Act
        var result = await _MaterialManagerClientTextures!.GetTextures(new TextureFilter());

        // Assert
        result.ShouldNotBeNull("because paged texture result should be returned");
        result.Textures.ShouldNotBeNull("because textures list should be present");
        result.Textures.Count.ShouldBeLessThanOrEqualTo(100, "because default page size is 100");
    }

    /// <summary>
    /// Tests retrieval of textures with custom page size.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithCustomPageSize_ReturnsPagedResult()
    {
        // Arrange
        const int pageSize = 10;

        // Act
        var result = await _MaterialManagerClientTextures!.GetTextures(new TextureFilter(), pageSize: pageSize);

        // Assert
        result.ShouldNotBeNull("because paged texture result should be returned");
        result.Textures.ShouldNotBeNull("because textures list should be present");
        result.Textures.Count.ShouldBeLessThanOrEqualTo(pageSize, "because page size limits result count");
    }

    /// <summary>
    /// Verifies that textures can be retrieved as a single page and as a complete asynchronous sequence.
    /// </summary>
    [IntegrationTest("MaterialManager.Textures")]
    [TestMethod]
    public async Task Textures_GetAllTextures()
    {
        const int pageSize = 100;

        var stopwatch = Stopwatch.StartNew();

        var firstPageTextures = (await _MaterialManagerClientTextures!.GetTextures(new TextureFilter(), pageSize: pageSize)).Textures;

        TestContext!.WriteLine($"Time taken to fetch {firstPageTextures.Count} of {pageSize} textures: {stopwatch.Elapsed}");

        stopwatch.Restart();

        var allTextures = await _MaterialManagerClientTextures.GetTextures(TestContext.CancellationToken).ToListAsync();

        TestContext.WriteLine($"Time taken to fetch all {allTextures.Count} textures: {stopwatch.Elapsed}");

        firstPageTextures.ShouldNotBeNull("because first page textures should be returned");
        allTextures.ShouldNotBeNull("because all textures should be returned");

        firstPageTextures.Count.ShouldBeLessThanOrEqualTo(pageSize,
            $"because GetTextures with page size {pageSize} should return at most {pageSize} textures");
        allTextures.Count.ShouldBeGreaterThanOrEqualTo(firstPageTextures.Count,
            "because streaming all textures should include at least the first page");
    }

    /// <summary>
    /// Tests continuation token pagination through all available textures.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithContinuationToken_PagesAllResults()
    {
        // Arrange
        // Ensure textures are available
        await EnsureTestTexturesExistAsync();

        const int pageSize = 1;
        var allTextureIds = new HashSet<string>();
        string? continuationToken = null;
        var pageCount = 0;

        // Act - Page through all textures
        do
        {
            pageCount++;
            var result = await _MaterialManagerClientTextures!.GetTextures(filter: null, pageSize: pageSize, continuationToken: continuationToken);

            result.ShouldNotBeNull("because paged result should be returned");
            result.Textures.ShouldNotBeNull("because textures list should be present");
            result.Textures.Count.ShouldBeLessThanOrEqualTo(pageSize, "because page size should be respected");

            foreach (var texture in result.Textures)
            {
                allTextureIds.Add(texture.Id);
            }

            continuationToken = result.ContinuationToken;

            if (pageCount > 100)
                break; // Safety limit to prevent infinite loops
        } while (continuationToken != null);

        // Assert
        allTextureIds.Count.ShouldBeGreaterThanOrEqualTo(2,
            "because at least the 2 test textures should be retrieved");
        pageCount.ShouldBeGreaterThanOrEqualTo(1,
            "because pagination should have at least one page");
    }

    /// <summary>
    /// Tests retrieval of textures filtered by catalog.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithCatalogFilter_ReturnsOnlyFilteredCatalog()
    {
        // Arrange
        await EnsureTestTexturesExistAsync();

        // Act
        var result = await _MaterialManagerClientTextures!.GetTextures(new TextureFilter { Catalog = _TestCatalog });

        // Assert
        result.ShouldNotBeNull("because paged result should be returned");
        result.Textures.ShouldNotBeNull("because textures list should be present");
        result.Textures.All(t => t.Catalog == _TestCatalog).ShouldBeTrue(
            $"because all returned textures should have catalog '{_TestCatalog}'");
        result.Textures.Count.ShouldBeGreaterThanOrEqualTo(2,
            "because at least the 2 test textures should be returned");
    }

    /// <summary>
    /// Tests retrieval of textures filtered by catalog and decor code.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithCatalogAndDecorCodeFilter_ReturnsOnlyFilteredResults()
    {
        // Arrange
        await EnsureTestTexturesExistAsync();

        // Act
        var result = await _MaterialManagerClientTextures!.GetTextures(new TextureFilter { Catalog = _TestCatalog, DecorCode = _TestCatalogDecor1 });

        // Assert
        result.ShouldNotBeNull("because paged result should be returned");
        result.Textures.ShouldNotBeNull("because textures list should be present");
        result.Textures.All(t => t.Catalog == _TestCatalog && t.DecorCode == _TestCatalogDecor1).ShouldBeTrue(
            $"because all returned textures should have catalog '{_TestCatalog}' and decor code '{_TestCatalogDecor1}'");
    }

    /// <summary>
    /// Tests retrieval of textures filtered by catalog, decor code, and embossing.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithFullFilter_ReturnsOnlyMatchingTexture()
    {
        // Arrange
        await EnsureTestTexturesExistAsync();

        // Act
        var result = await _MaterialManagerClientTextures!.GetTextures(
            new TextureFilter { Catalog = _TestCatalog, DecorCode = _TestCatalogDecor1, Embossing = _TestCatalogEmb1 });

        // Assert
        result.ShouldNotBeNull();
        result.Textures.ShouldNotBeNull();
        result.Textures.All(t =>
            t.Catalog == _TestCatalog &&
            t.DecorCode == _TestCatalogDecor1 &&
            t.Embossing == _TestCatalogEmb1
        ).ShouldBeTrue("because all fields of the filter should be applied");
    }

    /// <summary>
    /// Tests that null catalog parameter works correctly.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithNullCatalog_ReturnsAllTextures()
    {
        // Act
        var result = await _MaterialManagerClientTextures!.GetTextures(new TextureFilter { Catalog = null });

        // Assert
        result.ShouldNotBeNull("because paged result should be returned");
        result.Textures.ShouldNotBeNull("because textures list should be present");
    }

    /// <summary>
    /// Tests that null catalog and decor code parameters work correctly.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithNullCatalogAndDecorCode_ReturnsAllTextures()
    {
        // Act
        var result = await _MaterialManagerClientTextures!.GetTextures(new TextureFilter { Catalog = null, DecorCode = null });

        // Assert
        result.ShouldNotBeNull("because paged result should be returned");
        result.Textures.ShouldNotBeNull("because textures list should be present");
    }

    /// <summary>
    /// Tests GetTextures with invalid page size (zero).
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithZeroPageSize_ThrowsException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.GetTextures(new TextureFilter(), pageSize: 0);
        await Should.ThrowAsync<HttpRequestException>(act,
            "because page size must be greater than 0");
    }

    /// <summary>
    /// Tests GetTextures with invalid page size (negative).
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithNegativePageSize_ThrowsException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.GetTextures(new TextureFilter(), pageSize: -5);
        await Should.ThrowAsync<HttpRequestException>(act,
            "because page size must be greater than 0");
    }

    /// <summary>
    /// Tests GetTextures with page size exceeding maximum allowed.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithPageSizeExceedingMaximum_ThrowsException()
    {
        // Act & Assert
        const int excessivePageSize = 1000;
        var act = async () => await _MaterialManagerClientTextures!.GetTextures(new TextureFilter(), pageSize: excessivePageSize);
        await Should.ThrowAsync<HttpRequestException>(act,
            "because page size must not exceed maximum allowed (100)");
    }

    /// <summary>
    /// Tests that GetTextures with decor code but without catalog does not throw.
    /// </summary>
    [TestMethod]
    public async Task GetTextures_WithDecorCodeButNoCatalog_DoesNotThrow()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.GetTextures(new TextureFilter { Catalog = null, DecorCode = "some-decor" });
        await Should.NotThrowAsync(act);
    }

    /// <summary>
    /// Tests deletion of a texture by ID.
    /// </summary>
    [TestMethod]
    public async Task DeleteTexture_WithValidId_DeletesSuccessfully()
    {
        // Arrange
        const string deleteTestId = "TX-DELETE-TEST";
        const string deleteTestDecor = "delete-001";
        const string deleteTestEmb = "st1";

        // Create the texture and capture the actual texture ID
        var material = CreateTestMaterialDefinition(deleteTestId, _TestCatalog, deleteTestDecor, deleteTestEmb);
        var importedTexture = await _MaterialManagerClientTextures!.ImportOrUpdate(material);
        var textureId = importedTexture.Id;

        textureId.ShouldNotBeNullOrWhiteSpace("because texture ID should exist");

        // Wait for persistence
        await Task.Delay(500);

        // Verify it exists before deletion
        var retrievedTexture = await _MaterialManagerClientTextures.GetTexture(textureId);
        retrievedTexture.ShouldNotBeNull("because texture should exist before deletion");

        // Act - Delete using the Texture.Id
        var act = async () => await _MaterialManagerClientTextures!.DeleteTexture(textureId);

        // Assert - Should not throw
        await Should.NotThrowAsync(act, "because texture deletion should succeed");

        // Verify texture is actually deleted
        var act2 = async () => await _MaterialManagerClientTextures!.GetTexture(textureId);
        await Should.ThrowAsync<Exception>(act2,
            "because texture should no longer exist after deletion");
    }

    /// <summary>
    /// Tests that deleting an already-deleted texture completes without throwing.
    /// Demonstrates idempotency of the delete operation.
    /// </summary>
    [TestMethod]
    public async Task DeleteTexture_WhenAlreadyDeleted_DoesNotThrow()
    {
        // Arrange
        const string deleteIdempotentTestId = "TX-DELETE-IDEMPOTENT-TEST";
        const string deleteIdempotentDecor = "delete-idempotent-001";
        const string deleteIdempotentEmb = "st2";

        // Create the texture
        var material = CreateTestMaterialDefinition(deleteIdempotentTestId, _TestCatalog, deleteIdempotentDecor, deleteIdempotentEmb);
        var importedTexture = await _MaterialManagerClientTextures!.ImportOrUpdate(material);
        var textureId = importedTexture.Id;

        await Task.Delay(500);

        // Delete it the first time
        await _MaterialManagerClientTextures.DeleteTexture(textureId);

        // Wait for deletion to propagate
        await Task.Delay(500);

        // Act - Delete again (should be idempotent)
        var act = async () => await _MaterialManagerClientTextures!.DeleteTexture(textureId);

        // Assert - Should not throw even though texture is already deleted
        await Should.NotThrowAsync(act,
            "because delete operation should be idempotent and not throw on non-existent resource");
    }

    /// <summary>
    /// Tests that texture metadata is correctly populated after import.
    /// </summary>
    [TestMethod]
    public async Task ImportOrUpdate_PopulatesTextureMetadata()
    {
        // Arrange
        const string metadataTestId = "TX-METADATA-TEST";
        const string metadataTestDecor = "metadata-001";
        const string metadataTestEmb = "st8";
        var expectedTestCatalog = _TestCatalog;

        var material = CreateTestMaterialDefinition(metadataTestId, expectedTestCatalog, metadataTestDecor, metadataTestEmb);

        // Act
        var result = await _MaterialManagerClientTextures!.ImportOrUpdate(material);

        // Assert - Verify all metadata fields are populated
        result.ShouldNotBeNull("because import should return texture");
        result.Id.ShouldNotBeNullOrWhiteSpace("because texture ID should be populated");
        result.Catalog.ShouldBe(expectedTestCatalog, "because catalog should match");
        result.DecorCode.ShouldBe(metadataTestDecor, "because decor code should be populated");
        result.Embossing.ShouldBe(metadataTestEmb, "because embossing should be populated");
        result.Name.ShouldNotBeNullOrWhiteSpace("because texture name should be populated");
        result.Lookup.ShouldNotBeNullOrWhiteSpace("because texture lookup should be populated");
        result.ModifiedAt.ShouldNotBe(default(DateTime), "because modified date should be set");
    }

    /// <summary>
    /// Tests that DeleteTexture throws when ID is null.
    /// </summary>
    [TestMethod]
    public async Task DeleteTexture_WithNullId_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.DeleteTexture(null!);
        await Should.ThrowAsync<ArgumentNullException>(act);
    }

    /// <summary>
    /// Tests that DeleteTexture throws when ID is empty.
    /// </summary>
    [TestMethod]
    public async Task DeleteTexture_WithEmptyId_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.DeleteTexture(string.Empty);
        await Should.ThrowAsync<ArgumentNullException>(act);
    }

    /// <summary>
    /// Tests that DeleteTexture throws when ID is whitespace.
    /// </summary>
    [TestMethod]
    public async Task DeleteTexture_WithWhitespaceId_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.DeleteTexture("   ");
        await Should.ThrowAsync<ArgumentNullException>(act);
    }

    /// <summary>
    /// Tests importing a single texture using Roomle material definition.
    /// </summary>
    [TestMethod]
    public async Task ImportOrUpdate_WithSingleMaterial_CreatesTexture()
    {
        // Arrange
        const string importTestId = "TX-IMPORT-SINGLE";
        const string importTestDecor = "import-001";
        const string importTestEmb = "st2";
        var material = CreateTestMaterialDefinition(importTestId, _TestCatalog, importTestDecor, importTestEmb);

        // Act
        var result = await _MaterialManagerClientTextures!.ImportOrUpdate(material);

        // Assert
        result.ShouldNotBeNull("because import should return a texture");
        result.Catalog.ShouldBe(_TestCatalog, "because imported texture should have the specified catalog");
        result.Name.ShouldNotBeNullOrWhiteSpace("because texture should have a name");
    }

    /// <summary>
    /// Tests importing multiple textures using batch Roomle material definitions.
    /// </summary>
    [TestMethod]
    public async Task ImportOrUpdate_WithMultipleMaterials_CreatesMultipleTextures()
    {
        // Arrange
        var materials = new MaterialDefinitionsRoomle
        {
            Materials =
            [
                CreateTestMaterial("TX-BATCH-001", _TestCatalog, "batch-001", "st3"),
                CreateTestMaterial("TX-BATCH-002", _TestCatalog, "batch-002", "st4"),
                CreateTestMaterial("TX-BATCH-003", _TestCatalog, "batch-003", "st5")
            ]
        };

        // Act
        var results = await _MaterialManagerClientTextures!.ImportOrUpdate(materials);

        // Assert
        results.ShouldNotBeNull("because batch import should return textures");
        results.Length.ShouldBe(3, "because we imported 3 materials");
        results.All(t => t.Catalog == _TestCatalog).ShouldBeTrue("because all textures should belong to test catalog");
    }

    /// <summary>
    /// Tests that reimporting a texture with ImportOrUpdate updates the existing texture.
    /// </summary>
    [TestMethod]
    public async Task ImportOrUpdate_WithExistingTexture_UpdatesTexture()
    {
        // Arrange
        const string updateTestId = "TX-UPDATE-TEST";
        const string updateTestDecor = "update-001";
        const string updateTestEmb = "st6";
        var originalMaterial = CreateTestMaterialDefinition(updateTestId, _TestCatalog, updateTestDecor, updateTestEmb);
        originalMaterial.Material!.Label = "Original Label";

        // Act 1 - Import original
        var originalTexture = await _MaterialManagerClientTextures!.ImportOrUpdate(originalMaterial);
        originalTexture.Name.ShouldBe("Original Label", "because original should have original name");

        // Act 2 - Update with new material
        var updatedMaterial = CreateTestMaterialDefinition(updateTestId, _TestCatalog, updateTestDecor, updateTestEmb);
        updatedMaterial.Material!.Label = "Updated Label";
        var updatedTexture = await _MaterialManagerClientTextures.ImportOrUpdate(updatedMaterial);

        // Assert
        updatedTexture.Name.ShouldBe("Updated Label", "because texture name should be updated");
    }

    /// <summary>
    /// Tests ImportOrUpdate with null material throws exception.
    /// </summary>
    [TestMethod]
    public async Task ImportOrUpdate_WithNullMaterial_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.ImportOrUpdate((MaterialDefinitionRoomle)null!);
        await Should.ThrowAsync<ArgumentNullException>(act,
            "because material definition cannot be null");
    }

    /// <summary>
    /// Tests ImportOrUpdate batch with null materials collection throws exception.
    /// </summary>
    [TestMethod]
    public async Task ImportOrUpdate_BatchWithNullMaterials_ThrowsArgumentNullException()
    {
        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.ImportOrUpdate((MaterialDefinitionsRoomle)null!);
        await Should.ThrowAsync<ArgumentNullException>(act,
            "because materials collection cannot be null");
    }

    /// <summary>
    /// Tests ImportOrUpdate batch with empty materials list throws exception.
    /// </summary>
    [TestMethod]
    public async Task ImportOrUpdate_BatchWithEmptyMaterials_ThrowsException()
    {
        // Arrange
        var emptyMaterials = new MaterialDefinitionsRoomle { Materials = [] };

        // Act & Assert
        var act = async () => await _MaterialManagerClientTextures!.ImportOrUpdate(emptyMaterials);
        await Should.ThrowAsync<HttpRequestException>(act,
            "because at least one material must be provided for batch import");
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Computes the expected texture ID based on catalog, decor code, and embossing.
    /// This mirrors the logic in MaterialDefinitionRoomleExtensions.MapMaterialToTexture().
    /// </summary>
    private static string ComputeExpectedTextureId(string catalog, string decorCode, string embossing)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(catalog))
            parts.Add(catalog);
        if (!string.IsNullOrWhiteSpace(decorCode))
            parts.Add(decorCode);
        if (!string.IsNullOrWhiteSpace(embossing))
            parts.Add(embossing);

        return string.Join("_", parts);
    }

    /// <summary>
    /// Creates a minimal but complete Roomle material for testing.
    /// </summary>
    private static Contracts.Surfaces.Textures.Roomle.Material CreateTestMaterial(
        string textureId,
        string catalog,
        string decorCode,
        string embossing)
    {
        return new Contracts.Surfaces.Textures.Roomle.Material
        {
            Id = $"{catalog.ToLowerInvariant()}:{decorCode.ToLowerInvariant()}_{embossing.ToLowerInvariant()}",
            ExternalIdentifier = textureId.ToLowerInvariant(),
            Label = $"Test Material {textureId}",
            Catalog = catalog,
            Active = true,
            Hidden = false,
            Created = _FixedTestTimestamp,
            Updated = _FixedTestTimestamp,
            Version = 0,
            Language = "en",
            VisibilityStatus = 0,
            Shading = new Shading
            {
                Version = "2.0.0",
                Alpha = 1.0,
                AlphaCutoff = 0,
                AlphaMode = "OPAQUE",
                Basecolor = new ColorRgb { R = 1.0, G = 1.0, B = 1.0 },
                Transmission = 0.0,
                TransmissionIOR = 1.5,
                Metallic = 0,
                Roughness = 1.0,
                DoubleSided = true,
                Occlusion = 0,
                EmissiveColor = new ColorRgb { R = 0.0, G = 0.0, B = 0.0 },
                EmissiveIntensity = 1.0,
                ClearcoatIntensity = 0.0,
                ClearcoatRoughness = 0.0,
                ClearcoatNormalScale = 0.0,
                SheenColor = new ColorRgb { R = 0, G = 0, B = 0 },
                SheenIntensity = 0.0,
                SheenRoughness = 0.65,
                NormalScale = 1.0,
                ThicknessFactor = 0.0,
                AttenuationColor = new ColorRgb { R = 0, G = 0, B = 0 },
                AttenuationDistance = 0.0
            },
            TextureObjects = [],
            Textures = [],
            Tags = ["test"],
            Links = new MaterialLinks { Textures = "/materials/test/textures" }
        };
    }

    /// <summary>
    /// Creates a Roomle material definition for testing.
    /// </summary>
    private static MaterialDefinitionRoomle CreateTestMaterialDefinition(
        string textureId,
        string catalog,
        string decorCode,
        string embossing)
    {
        return new MaterialDefinitionRoomle
        {
            Material = CreateTestMaterial(textureId, catalog, decorCode, embossing)
        };
    }

    /// <summary>
    /// Ensures a test texture exists by attempting to retrieve it.
    /// If it doesn't exist, creates it using ImportOrUpdate.
    /// </summary>
    private async Task EnsureTestTextureExists(
        string textureId,
        string catalog,
        string decorCode,
        string embossing)
    {
        var expectedId = ComputeExpectedTextureId(catalog, decorCode, embossing);

        try
        {
            await _MaterialManagerClientTextures!.GetTexture(expectedId);
            return;
        }
        catch (Exception ex)
        {
            // Texture doesn't exist, create it
        }

        // Create the texture
        var material = CreateTestMaterialDefinition(textureId, catalog, decorCode, embossing);
        var createdTexture = await _MaterialManagerClientTextures!.ImportOrUpdate(material);

        // Use the returned texture ID
        var actualId = createdTexture.Id;

        // Wait for persistence
        await Task.Delay(500);

        // Verify it was created using the actual returned ID
        try
        {
            await _MaterialManagerClientTextures.GetTexture(actualId);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Failed to retrieve created texture {actualId}. Error: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Ensures test textures exist or marks test as inconclusive if unavailable.
    /// This should be called at the start of any integration test that requires specific test textures.
    /// Checks BOTH test textures and recreates any that are missing.
    /// </summary>
    private async Task EnsureTestTexturesExistAsync()
    {
        var firstTextureId = ComputeExpectedTextureId(_TestCatalog, _TestCatalogDecor1, _TestCatalogEmb1);
        var secondTextureId = ComputeExpectedTextureId(_TestCatalog, _TestCatalogDecor2, _TestCatalogEmb2);

        // Check which textures exist and which need to be created
        var firstTextureExists = await CheckTextureExists(firstTextureId);
        var secondTextureExists = await CheckTextureExists(secondTextureId);

        // If both textures exist, we're done
        if (firstTextureExists && secondTextureExists)
        {
            return;
        }

        // Try to create missing textures
        try
        {
            if (!firstTextureExists)
            {
                await EnsureTestTextureExists("TX-TEST-001", _TestCatalog, _TestCatalogDecor1, _TestCatalogEmb1);
            }

            if (!secondTextureExists)
            {
                await EnsureTestTextureExists("TX-TEST-002", _TestCatalog, _TestCatalogDecor2, _TestCatalogEmb2);
            }
        }
        catch (Exception ex)
        {
            // Log the error but don't fail the test setup
            $"Warning: Failed to create test textures: {ex.Message}".Trace();
            Assert.Inconclusive($"Integration test skipped: Could not create test textures. {ex.Message}");
        }
    }

    /// <summary>
    /// Checks if a texture exists and handles authorization failures gracefully.
    /// </summary>
    private async Task<bool> CheckTextureExists(string textureId)
    {
        try
        {
            await _MaterialManagerClientTextures!.GetTexture(textureId);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion
}