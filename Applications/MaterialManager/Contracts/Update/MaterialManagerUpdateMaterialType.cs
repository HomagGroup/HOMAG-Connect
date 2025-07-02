using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;

namespace HomagConnect.MaterialManager.Contracts.Update;

/// <summary>
/// Update object to update a material type in materialManager.
/// </summary>
public class MaterialManagerUpdateMaterialType
{
    /// <summary>
    /// Gets or sets the additional data.
    /// </summary>
    public ICollection<AdditionalDataEntity>? AdditionalData { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [StringLength(50)]
    public string? ArticleNumber { get; set; } = null;

    /// <summary>
    /// Gets or sets the additional comments.
    /// </summary>
    [StringLength(300)]
    public string? Comments { get; set; } = null;

    /// <summary>
    /// Gets or sets the costs of the board per m² or ft².
    /// </summary>
    [Range(0, double.PositiveInfinity)]
    public double? Costs { get; set; } = null;

    /// <summary>
    /// Gets or sets the decor code.
    /// </summary>
    [StringLength(50)]
    public string? DecorCode { get; set; } = null;

   

    /// <summary>
    /// Gets or sets the decor name.
    /// </summary>
    [StringLength(50)]
    public string? DecorName { get; set; } = null;

    /// <summary>
    /// Gets or sets the gtin.
    /// </summary>
    [StringLength(50)]
    public string? Gtin { get; set; } = null;

    /// <summary>
    /// Gets or sets the name of the manufacturer.
    /// </summary>
    [StringLength(50)]
    public string? ManufacturerName { get; set; } = null;

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    [StringLength(50)]
    public string? ProductName { get; set; } = null;

    /// <summary>
    /// Gets or set the thumbnail uri.
    /// </summary>
    [Obsolete("This parameter is obsolete. Use AdditionalData instead.")]
    public Uri? Thumbnail { get; set; } = null;
}