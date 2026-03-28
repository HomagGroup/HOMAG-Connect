using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Surfaces.Textures.Roomle;

// NOTE: This is preliminary code and is subject to change

/// <summary>
/// Roomle material DTO used for JSON serialization/deserialization.
/// Represents a catalog material with shading parameters, textures, and metadata.
/// </summary>
public class Material
{
    /// <summary>
    /// Indicates whether the material is active/available.
    /// </summary>
    public bool Active { get; set; }

    /// <summary>
    /// Free-form additional information entries.
    /// </summary>
    /// <remarks>
    /// Ignored during deserialization because the upstream API returns mixed types
    /// (empty array, strings, or complex objects) that cannot be reliably bound.
    /// This data is not critical for our use case and can be safely ignored.
    /// </remarks>
    [JsonIgnore]
    public List<string>? AdditionalInfos { get; set; }

    /// <summary>
    /// Catalog identifier (e.g., vendor or collection).
    /// </summary>
    public string? Catalog { get; set; }

    /// <summary>
    /// Creation timestamp of the material record.
    /// </summary>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// External identifier from upstream systems.
    /// </summary>
    public string? ExternalIdentifier { get; set; }

    /// <summary>
    /// Indicates whether the material is hidden from public listings.
    /// </summary>
    public bool Hidden { get; set; }

    /// <summary>
    /// Roomle material id.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Human-readable label for the material.
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// Language/culture code of the label and related text.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Links to related resources, such as textures endpoint.
    /// </summary>
    public MaterialLinks? Links { get; set; }

    /// <summary>
    /// Physically based rendering parameters (alpha, metallic, roughness, colors, etc.).
    /// </summary>
    public Shading? Shading { get; set; }

    /// <summary>
    /// Tag list for categorization/filtering.
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// Concrete texture object variants including dimensions and mapping.
    /// </summary>
    public List<TextureObject>? TextureObjects { get; set; }

    /// <summary>
    /// Texture ids associated with this material.
    /// </summary>
    public List<int>? Textures { get; set; }

    /// <summary>
    /// URL (string) of the material thumbnail image.
    /// </summary>
    public string? Thumbnail { get; set; }

    /// <summary>
    /// Last update timestamp of the material record.
    /// </summary>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Schema or content version of the material entry.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Visibility status code (Roomle-specific numeric state).
    /// </summary>
    public int VisibilityStatus { get; set; }

    #region Additional Properties

    /// <summary>
    /// Gets or sets the additional properties configured in the application.
    /// </summary>
    [JsonExtensionData]
    [JsonProperty(Order = 999)]
    public IDictionary<string, object>? AdditionalProperties { get; set; }

    #endregion
}