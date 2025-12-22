using System.Globalization;

using HomagConnect.Base.Contracts;
using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Extensions;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures;
using HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// ReSharper disable LocalizableElement

// TODO: Remove this temporary test implementation when a real one is available

namespace HomagConnect.MaterialManager.Tests.Surfaces.Temp;

/// <summary>
/// MaterialDefinitionRoomle extension methods for mapping and uploading.
/// </summary>
public static class MaterialDefinitionRoomleExtensions
{
    /// <summary>
    /// Maps a Roomle material definition to a Texture and uploads associated files.
    /// </summary>
    public static async Task<Texture> MapToTextureAndUploadReferenceFiles(
        this MaterialDefinitionRoomle materialDefinitionRoomle,
        TextureStorageManager textureStorageManager,
        FileReference[]? fileReferences = null)
    {
        if (materialDefinitionRoomle is null)
        {
            throw new ArgumentNullException(nameof(materialDefinitionRoomle));
        }

        if (textureStorageManager is null)
        {
            throw new ArgumentNullException(nameof(textureStorageManager));
        }

        var material = materialDefinitionRoomle.Material
                       ?? throw new ArgumentException("MaterialDefinitionRoomle does not contain a Material.", nameof(materialDefinitionRoomle));

        if (string.IsNullOrWhiteSpace(material.Id))
        {
            throw new ArgumentException("Material.Id is null or empty.", nameof(materialDefinitionRoomle));
        }

        if (string.IsNullOrWhiteSpace(material.Label))
        {
            throw new ArgumentException("Material.Label is null or empty.", nameof(materialDefinitionRoomle));
        }

        var texture = MapToTexture(material);
        texture.AdditionalData = [];

        var versionTag = texture.ModifiedAt.ToString("yyyyMMddHHmm", CultureInfo.InvariantCulture);
        var relativePath = $"/{texture.Catalog}/{texture.DecorCode}_{texture.Embossing}_v{versionTag}";

        await UploadThumbnailIfPresent(material, texture, relativePath, textureStorageManager);
        await UploadTextureObjectsIfPresent(material, relativePath, textureStorageManager);
        await UploadMaterialDefinition(materialDefinitionRoomle, texture, relativePath, textureStorageManager);

        return texture;
    }

    private static Texture MapToTexture(Contracts.Surfaces.Textures.Roomle.Material material)
    {
        var texture = new Texture
        {
            Name = material.Label!,
            Catalog = material.Catalog,
            ExternalId = material.Id,
            ModifiedAt = material.Updated
        };

        if (!string.IsNullOrWhiteSpace(texture.ExternalId))
        {
            var strings = texture.ExternalId.Split(":");
            var b = texture.ExternalId;
            if (strings.Length > 1)
            {
                b = strings[1];
            }

            var parts = b.Split('_', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length > 0) texture.DecorCode = parts[0];
            if (parts.Length > 1) texture.Embossing = parts[1];
        }

        var id = new List<string>(3);
        if (!string.IsNullOrWhiteSpace(texture.Catalog)) id.Add(texture.Catalog!);
        if (!string.IsNullOrWhiteSpace(texture.DecorCode)) id.Add(texture.DecorCode!);
        if (!string.IsNullOrWhiteSpace(texture.Embossing)) id.Add(texture.Embossing!);

        texture.Id = string.Join("_", id);
        if (string.IsNullOrWhiteSpace(texture.Id))
        {
            texture.Id = texture.ExternalId ?? Guid.NewGuid().ToString("N");
        }

        return texture;
    }

    private static async Task UploadMaterialDefinition(
        MaterialDefinitionRoomle materialDefinitionRoomle,
        Texture texture,
        string relativePath,
        TextureStorageManager textureStorageManager)
    {
        var materialDefinitionTarget = relativePath + ".rml";

        await using var materialDefinitionStream = materialDefinitionRoomle.ToStream();
        var materialDefinitionTargetUri = await textureStorageManager.Upload(materialDefinitionStream, materialDefinitionTarget);

        texture.AdditionalData ??= [];

        texture.AdditionalData.Add(
            new AdditionalDataSurfaceTexture
            {
                Name = texture.Name,
                Category = "Roomle",
                DownloadUri = materialDefinitionTargetUri
            });
    }

    private static async Task UploadTextureObjectsIfPresent(
        Contracts.Surfaces.Textures.Roomle.Material material,
        string relativePath,
        TextureStorageManager textureStorageManager)
    {
        if (material.TextureObjects != null && material.TextureObjects.Any())
        {
            foreach (var materialTextureObject in material.TextureObjects)
            {
                if (!string.IsNullOrWhiteSpace(materialTextureObject.Image))
                {
                    var materialTextureObjectSourceUri = new Uri(materialTextureObject.Image);
                    var materialTextureObjectSourceExtension = Path.GetExtension(materialTextureObjectSourceUri.AbsolutePath);
                    await using var materialTextureObjectSourceStream = await materialTextureObjectSourceUri.DownloadStreamAsync();

                    var materialTextureObjectTarget = relativePath + "_" + materialTextureObject.Id + materialTextureObjectSourceExtension;
                    var materialTextureObjectTargetUri = await textureStorageManager.Upload(materialTextureObjectSourceStream, materialTextureObjectTarget);

                    if (materialTextureObjectTargetUri != null)
                    {
                        materialTextureObject.Image = materialTextureObjectTargetUri.ToString();
                        materialTextureObject.Url = materialTextureObjectTargetUri.ToString();
                    }
                }
            }
        }
    }

    private static async Task UploadThumbnailIfPresent(
        Contracts.Surfaces.Textures.Roomle.Material material,
        Texture texture,
        string relativePath,
        TextureStorageManager textureStorageManager)
    {
        if (!string.IsNullOrWhiteSpace(material.Thumbnail))
        {
            var thumbnailSourceUri = new Uri(material.Thumbnail);
            var thumbnailSourceExtension = Path.GetExtension(thumbnailSourceUri.AbsolutePath);
            await using var thumbnailSourceStream = await thumbnailSourceUri.DownloadStreamAsync();

            var thumbnailTarget = relativePath + thumbnailSourceExtension;
            var thumbnailTargetUri = await textureStorageManager.Upload(thumbnailSourceStream, thumbnailTarget);

            if (thumbnailTargetUri != null)
            {
                texture.AdditionalData ??= [];

                texture.AdditionalData.Add(
                    new AdditionalDataImage
                    {
                        Name = texture.Name,
                        Category = "Thumbnail",
                        DownloadUri = thumbnailTargetUri
                    });

                material.Thumbnail = thumbnailTargetUri.ToString();
            }
        }
    }
}