using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Details;

/// <summary>
/// Production entity production order.
/// </summary>
public class ProductionEntityProductionOrder : ProductionEntity
{
    /// <summary>
    /// Gets or sets the article group.
    /// </summary>
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the barcode.
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the back.
    /// </summary>
    [JsonProperty(Order = 32)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeBack { get; set; }

    /// <summary>
    /// Gets or sets how the edgebands should get applied.
    /// </summary>
    [JsonProperty(Order = 35)]
    public string? EdgeDiagram { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the front.
    /// </summary>
    [JsonProperty(Order = 31)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeFront { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the left.
    /// </summary>
    [JsonProperty(Order = 33)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeLeft { get; set; }

    /// <summary>
    /// Gets or sets the edgeband code of the edgeband type which should get applied on the right.
    /// </summary>
    [JsonProperty(Order = 34)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeRight { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    public Grain Grain { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>

    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Gets or set the production resources.
    /// </summary>
    [JsonProperty(Order = 99)]
    public Collection<ProductionEntity>? ProductionResources { get; set; }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    public double? Thickness { get; set; }

    /// <inheritdoc />
    public override ProductionEntityType Type { get; set; } = ProductionEntityType.ProductionOrder;

    /// <summary>
    /// Gets or sets the width.
    /// </summary>

    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the CNC program name 1.
    /// </summary>
    public string? CncProgramName1 { get; set; }
}