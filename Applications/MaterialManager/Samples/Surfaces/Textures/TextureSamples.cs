using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

namespace HomagConnect.MaterialManager.Samples.Surfaces.Textures;

/// <summary>
/// Samples for using the MaterialManager textures client.
/// </summary>
public class TextureSamples
{
    /// <summary>
    /// The example shows how to delete a texture.
    /// </summary>
    public static async Task Textures_DeleteTexture(IMaterialManagerClientTextures textureClient, string textureId)
    {
        await textureClient.DeleteTexture(textureId);
    }

    /// <summary>
    /// The example shows how to retrieve a texture by id.
    /// </summary>
    public static async Task Textures_GetTextureById(IMaterialManagerClientTextures textureClient, string textureId)
    {
        var texture = await textureClient.GetTexture(textureId);
        texture.Trace();
    }

    /// <summary>
    /// The example shows how to retrieve textures with pagination.
    /// </summary>
    public static async Task Textures_GetTextures(IMaterialManagerClientTextures textureClient)
    {
        var result = await textureClient.GetTextures(new TextureFilter(), pageSize: 10);

        foreach (var texture in result.Textures)
        {
            texture.Trace();
        }

        // Use continuation token for next page if available
        if (!string.IsNullOrEmpty(result.ContinuationToken))
        {
            var nextResult = await textureClient.GetTextures(new TextureFilter(), pageSize: 10, continuationToken: result.ContinuationToken);
        }
    }

    /// <summary>
    /// The example shows how to retrieve textures filtered by catalog.
    /// </summary>
    public static async Task Textures_GetTexturesByCatalog(IMaterialManagerClientTextures textureClient, string catalog)
    {
        var result = await textureClient.GetTextures(new TextureFilter { Catalog = catalog }, pageSize: 10);

        foreach (var texture in result.Textures)
        {
            texture.Trace();
        }
    }

    /// <summary>
    /// The example shows how to retrieve textures filtered by catalog and decor code.
    /// </summary>
    public static async Task Textures_GetTexturesByCatalogAndDecorCode(
        IMaterialManagerClientTextures textureClient,
        string catalog,
        string decorCode)
    {
        var result = await textureClient.GetTextures(new TextureFilter { Catalog = catalog, DecorCode = decorCode }, pageSize: 10);

        foreach (var texture in result.Textures)
        {
            texture.Trace();
        }
    }

    /// <summary>
    /// The example shows how to import or update a texture.
    /// </summary>
    public static async Task Textures_ImportOrUpdateTexture(IMaterialManagerClientTextures textureClient)
    {
        var material = new Material
        {
            Id = "hpl:f274_9_st7",
            ExternalIdentifier = "HPL_F274_9_ST7",
            Label = "HPL F274 9 ST7",
            Catalog = "HPL",
            Active = true,
            Hidden = false,
            Created = DateTimeOffset.UtcNow,
            Updated = DateTimeOffset.UtcNow,
            Version = 1,
            Language = "en",
            VisibilityStatus = 0,
            Shading = new Shading
            {
                Version = "2.0.0",
                Alpha = 1.0,
                Basecolor = new ColorRgb { R = 1.0, G = 1.0, B = 1.0 },
                Metallic = 0,
                Roughness = 0.8
            },
            TextureObjects = [],
            Textures = [],
            Tags = ["HPL", "Decor"],
            Links = new MaterialLinks { Textures = "/materials/hpl/textures" }
        };

        var materialDefinition = new MaterialDefinitionRoomle
        {
            Material = material
        };

        var importedTexture = await textureClient.ImportOrUpdate(materialDefinition);
        importedTexture.Trace();
    }

    /// <summary>
    /// The example shows how to import or update multiple textures in a batch operation.
    /// </summary>
    public static async Task Textures_ImportOrUpdateTexturesBatch(IMaterialManagerClientTextures textureClient)
    {
        var materials = new List<Material>
        {
            new()
            {
                Id = "hpl:f274_9_st7",
                ExternalIdentifier = "HPL_F274_9_ST7",
                Label = "HPL F274 9 ST7",
                Catalog = "HPL",
                Active = true,
                Hidden = false,
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Version = 1,
                Language = "en",
                VisibilityStatus = 0,
                Shading = new Shading
                {
                    Version = "2.0.0",
                    Alpha = 1.0,
                    Basecolor = new ColorRgb { R = 1.0, G = 1.0, B = 1.0 },
                    Metallic = 0,
                    Roughness = 0.8
                },
                TextureObjects = [],
                Textures = [],
                Tags = ["HPL"],
                Links = new MaterialLinks { Textures = "/materials/hpl/textures" }
            },
            new()
            {
                Id = "hpl:f275_0_st9",
                ExternalIdentifier = "HPL_F275_0_ST9",
                Label = "HPL F275 0 ST9",
                Catalog = "HPL",
                Active = true,
                Hidden = false,
                Created = DateTimeOffset.UtcNow,
                Updated = DateTimeOffset.UtcNow,
                Version = 1,
                Language = "en",
                VisibilityStatus = 0,
                Shading = new Shading
                {
                    Version = "2.0.0",
                    Alpha = 1.0,
                    Basecolor = new ColorRgb { R = 0.9, G = 0.9, B = 0.9 },
                    Metallic = 0,
                    Roughness = 0.75
                },
                TextureObjects = [],
                Textures = [],
                Tags = ["HPL"],
                Links = new MaterialLinks { Textures = "/materials/hpl/textures" }
            }
        };

        var materialDefinitions = new MaterialDefinitionsRoomle
        {
            Materials = materials
        };

        var result = await textureClient.ImportOrUpdate(materialDefinitions);
        foreach (var texture in result)
        {
            texture.Trace();
        }
    }
}