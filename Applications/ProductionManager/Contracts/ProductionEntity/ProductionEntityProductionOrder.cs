using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionEntity;

// Note: This is preliminary code and is subject to change

/// <summary>
/// Production entity production order.
/// </summary>
public class ProductionEntityProductionOrder : ProductionEntity, ILaminatingProperties, IEdgebandingProperties, IDimensionsProperties, IMaterialProperties
{
    #region (10) Article

    /// <inheritdoc />
    [JsonProperty(Order = 0)]
    public override ProductionEntityType Type { get; set; } = ProductionEntityType.ProductionOrder;

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 16)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Gets the dimensions as a string.
    /// </summary>
    [JsonProperty(Order = 17)]
    public string? Dimensions
    {
        get
        {
            if (Length.HasValue && Width.HasValue && Thickness.HasValue)
            {
                return $"{Length} x {Width} x {Thickness}";
            }

            if (Length.HasValue && Width.HasValue)
            {
                return $"{Length} x {Width}";
            }

            return null;
        }
    }

    #region IMaterialProperties

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

    /// <summary>
    /// Gets or sets the finish length.
    /// </summary>
    [JsonProperty(Order = 19)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? FinishLength { get; set; }

    /// <summary>
    /// Gets or sets the finish width.
    /// </summary>
    [JsonProperty(Order = 20)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? FinishWidth { get; set; }

    #endregion

    #endregion

    #region (20) Production

    /// <summary>
    /// Gets or sets the quantity planned.
    /// </summary>
    public int? QuantityPlanned { get; set; }

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

    /// <summary>
    /// Gets or sets the second cut length.
    /// </summary>
    [JsonProperty(Order = 40)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? SecondCutLength { get; set; }

    /// <summary>
    /// Gets or sets the second cut width.
    /// </summary>
    [JsonProperty(Order = 41)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? SecondCutWidth { get; set; }

    /// <summary>
    /// Gets or sets the production route.
    /// </summary>
    [JsonProperty(Order = 42)]
    public string? ProductionRoute { get; set; }

    /// <summary>
    /// Gets or sets the production order type.
    /// </summary>
    [JsonProperty(Order = 43)]
    public string? ProductionOrderType { get; set; }

    /// <summary>
    /// Gets or sets the grain matching template.
    /// </summary>
    [JsonProperty(Order = 44)]
    public string Template { get; set; }

    #endregion

    #region (30) IEdgebandingProperties

    /// <inheritdoc />
    [JsonProperty(Order = 32)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeBack { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 35)]
    public string? EdgeDiagram { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 31)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 33)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 34)]
    [StringLength(50, MinimumLength = 1)]
    public string? EdgeRight { get; set; }

    #endregion

    #region (50) ILaminatingProperties

    /// <inheritdoc />
    [JsonProperty(Order = 51)]
    [StringLength(50, MinimumLength = 1)]
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Gets or sets the material code of the laminate type which should get applied on the bottom.
    /// </summary>
    [JsonProperty(Order = 52)]
    [StringLength(50, MinimumLength = 1)]
    public string? LaminateBottom { get; set; }

    #endregion

    #region (60) Production resources

    /// <summary>
    /// Gets or set the production resources.
    /// </summary>
    [JsonProperty(Order = 60)]
    public Collection<ProductionEntityResource>? ProductionResources { get; set; }

    #endregion

    #region (99) IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 999)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion
}