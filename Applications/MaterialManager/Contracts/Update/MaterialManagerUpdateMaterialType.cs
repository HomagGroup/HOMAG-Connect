using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Provides shared material values for an update. The default client serializer omits properties whose values are null.
/// </summary>
/// <example>
/// { "articleNumber": "ART-001", "comments": "Updated material notes" }
/// </example>
public class MaterialManagerUpdateMaterialType
{
    /// <summary>
    /// Supplementary name-and-value data associated with the material.
    /// </summary>
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Manufacturer's article number for identifying or ordering the material.
    /// </summary>
    [StringLength(50)]
    public string? ArticleNumber { get; set; } = null;

    /// <summary>
    /// Notes about the material.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; } = null;

    /// <summary>
    /// Cost associated with the material.
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? Costs { get; set; } = null;

    /// <summary>
    /// Code identifying the material's decorative design.
    /// </summary>
    [StringLength(50)]
    public string? DecorCode { get; set; } = null;

    /// <summary>
    /// Identifier associated with the decorative design.
    /// </summary>
    [StringLength(50)]
    public string? DecorId { get; set; } = null;

    /// <summary>
    /// Name of the material's decorative design.
    /// </summary>
    [StringLength(50)]
    public string? DecorName { get; set; } = null;

    /// <summary>
    /// Global Trade Item Number used to identify the material in the supply chain.
    /// </summary>
    [StringLength(50)]
    public string? Gtin { get; set; } = null;

    /// <summary>
    /// Name of the company that manufactures the material.
    /// </summary>
    [StringLength(50)]
    public string? ManufacturerName { get; set; } = null;

    /// <summary>
    /// Product name used by the manufacturer or supplier.
    /// </summary>
    [StringLength(200)]
    public string? ProductName { get; set; } = null;

    /// <summary>
    /// URI of an image that helps identify the material. This property is obsolete; use <see cref="AdditionalData" /> instead.
    /// </summary>
    [Obsolete("This parameter is obsolete. Use AdditionalData instead.")]
    public Uri? Thumbnail { get; set; } = null;
}