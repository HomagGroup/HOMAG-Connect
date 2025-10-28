using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.MaterialManager.Contracts.Request;

/// <summary>
/// Base properties for all material types.
/// </summary>
public abstract class MaterialManagerRequestMaterialType
{
    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [StringLength(50)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the additional comments.
    /// </summary>
    [StringLength(300)]
    public string Comments { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the costs of the board per m² or ft².
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? Costs { get; set; }

    /// <summary>
    /// Gets or sets the decor code.
    /// </summary>
    [StringLength(50)]
    public string? DecorCode { get; set; }

    /// <summary>
    /// Gets or sets the decor name.
    /// </summary>
    [StringLength(50)]
    public string? DecorName { get; set; }

    /// <summary>
    /// Gets or sets the gtin.
    /// </summary>
    [StringLength(50)]
    public string? Gtin { get; set; }

    /// <summary>
    /// Gets or sets the name of the manufacturer.
    /// </summary>
    [StringLength(50)]
    public string? ManufacturerName { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    [StringLength(200)]
    public string? ProductName { get; set; }

    /// <summary>
    /// Gets or set the thumbnail uri.
    /// </summary>
    [Obsolete("This parameter is obsolete. Use AdditionalData instead.")]
    public Uri? Thumbnail { get; set; }
}