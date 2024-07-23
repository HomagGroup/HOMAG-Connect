using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity production order.
/// </summary>
public class ProductionEntityProductionOrder : ProductionEntity
{
    #region (10) Article

    /// <inheritdoc />
    [JsonProperty(Order = 0)]
    public override ProductionEntityType Type { get; set; } = ProductionEntityType.ProductionOrder;

    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    [JsonProperty(Order = 11)]
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    [JsonProperty(Order = 12)]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the article group.
    /// </summary>
    [JsonProperty(Order = 13)]
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    [JsonProperty(Order = 14)]
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    [JsonProperty(Order = 15)]
    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 16)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    [JsonProperty(Order = 17)]
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [JsonProperty(Order = 18)]
    public Grain Grain { get; set; }

    #endregion

    #region (20) Production

    /// <summary>
    /// Gets or sets the CNC program name 1.
    /// </summary>
    [JsonProperty(Order = 38)]
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Gets or sets the CNC program name 2.
    /// </summary>
    [JsonProperty(Order = 39)]
    public string? CncProgramName2 { get; set; }

    #endregion

    #region (30) Edgeband data

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

    #endregion

    #region (60) Production resources

    /// <summary>
    /// Gets or set the production resources.
    /// </summary>
    [JsonProperty(Order = 60)]
    public Collection<ProductionEntityResource>? ProductionResources { get; set; }

    #endregion
}