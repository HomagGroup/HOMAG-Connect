using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Defines a texture.
/// </summary>
public class Texture : ISupportsAdditionalData
{
    /// <summary>
    /// Gets or sets the catalog of the texture.
    /// </summary>
    [JsonProperty(Order = 3)]
    [StringLength(50, MinimumLength = 0)]
    [Display(ResourceType = typeof(TextureDisplayNames), Name = nameof(TextureDisplayNames.Catalog))]
    public string? Catalog { get; set; }

    /// <summary>
    /// Gets or sets the decor code of the texture.
    /// </summary>
    [Display(ResourceType = typeof(TextureDisplayNames), Name = nameof(TextureDisplayNames.DecorCode))]
    [JsonProperty(Order = 10)]
    public string? DecorCode { get; set; }

    /// <summary>
    /// Gets or sets the embossing of the texture.
    /// </summary>
    [Display(ResourceType = typeof(TextureDisplayNames), Name = nameof(TextureDisplayNames.Embossing))]
    [JsonProperty(Order = 11)]
    public string? Embossing { get; set; }

    /// <summary>
    /// Gets or sets the reference of the texture.
    /// </summary>
    [JsonProperty(Order = 5)]
    public string? ExternalId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the texture.
    /// </summary>
    [Required]
    [JsonProperty(Order = 1)]
    [StringLength(50, MinimumLength = 1)]
    [Display(ResourceType = typeof(TextureDisplayNames), Name = nameof(TextureDisplayNames.Id))]
    [Key]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last modified date of the texture file.
    /// </summary>
    [JsonProperty(Order = 6)]
    [Display(ResourceType = typeof(TextureDisplayNames), Name = nameof(TextureDisplayNames.ModifiedAt))]
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the name of the texture.
    /// </summary>
    [JsonProperty(Order = 2)]
    [StringLength(100, MinimumLength = 1)]
    [Display(ResourceType = typeof(TextureDisplayNames), Name = nameof(TextureDisplayNames.Name))]
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    [JsonProperty(Order = 80)]
    public Collection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 90)]
    public IDictionary<string, object>? AdditionalProperties { get; set; }
}