using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems;

// Note: This is preliminary code and is subject to change

/// <summary>
/// A part of an order that is produced, with its dimensions, material, edgebanding, laminating, CNC programs, and lots.
/// </summary>
/// <example>
/// { "type": "Part", "id": "P-0001", "identifier": "4711000001", "articleNumber": "SIDE-L", "description": "Left side panel",
/// "length": 720.0, "width": 560.0, "barcode": "4711000001", "productionStatus": "New", "quantity": 1, "thickness": 19.0,
/// "dimensions": "720 x 560 x 19", "material": "P2_Hunton_Oak_dark_19.0", "grain": "Lengthwise", "quantityPlanned": 1,
/// "edgeFront": "ABS_Oak_2.0", "unitSystem": "Metric" }
/// </example>
public class Part : ProductionItemBase, IPartProperties
{
    #region (60) Production resources

    /// <summary>
    /// Production resources, such as hardware, that belong to the part. Optional.
    /// </summary>
    [JsonProperty(Order = 60)]
    public Collection<Resource>? ProductionResources { get; set; }

    #endregion

    #region (99) IContainsUnitSystemDependentProperties Members

    /// <inheritdoc />
    [JsonProperty(Order = 999)]
    [DefaultValue(UnitSystem.Metric)]
    public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

    #endregion

    #region (10) Article

    /// <inheritdoc />
    [JsonProperty(Order = 0)]
    [DefaultValue(ProductionItemType.Part)]
    public override ProductionItemType Type { get; set; } = ProductionItemType.Part;

    /// <summary>
    /// Description of the position (order item) the part belongs to. Optional.
    /// </summary>
    [JsonProperty(Order = 15)]
    [StringLength(100)]
    public string? OrderItemDescription { get; set; }

    /// <summary>
    /// Thickness of the part. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    /// <example>19.0</example>
    [JsonProperty(Order = 16)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? Thickness { get; set; }

    /// <summary>
    /// Dimensions as text in the form length x width x thickness, or length x width when no thickness is set. The value is
    /// computed from <see cref="ProductionItemBase.Length" />, <see cref="ProductionItemBase.Width" />, and <see cref="Thickness" />;
    /// a value sent by the consumer is ignored.
    /// </summary>
    /// <example>720 x 560 x 19</example>
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
        // ReSharper disable once ValueParameterNotUsed
        private set
        {
            // Needed for serialization. Do nothing. If removed the property is added to AdditionalProperties on deserialization.
        }
    }

    #region IMaterialProperties

    /// <summary>
    /// Material code of the part. Optional.
    /// </summary>
    /// <example>P2_Hunton_Oak_dark_19.0</example>
    [JsonProperty(Order = 17)]
    [StringLength(255)]
    public string? Material { get; set; }

    /// <summary>
    /// Grain direction of the part.
    /// </summary>
    /// <example>Lengthwise</example>
    [JsonProperty(Order = 18)]
    public Grain Grain { get; set; }

    /// <summary>
    /// Length of the finished part after all processing. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 19)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? FinishLength { get; set; }

    /// <summary>
    /// Width of the finished part after all processing. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 20)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? FinishWidth { get; set; }

    /// <summary>
    /// Surface of the bottom side of the part. Optional.
    /// </summary>
    [JsonProperty(Order = 21)]
    [StringLength(100)]
    public string? SurfaceBottom { get; set; }

    /// <summary>
    /// Surface of the top side of the part. Optional.
    /// </summary>
    [JsonProperty(Order = 22)]
    [StringLength(100)]
    public string? SurfaceTop { get; set; }

    #endregion

    #endregion

    #region (20) Production

    /// <summary>
    /// Number of pieces of the part that are planned for production. Optional.
    /// </summary>
    /// <example>1</example>
    [JsonProperty(Order = 23)]
    [Range(0, int.MaxValue)]
    public int? QuantityPlanned { get; set; }

    /// <summary>
    /// Length of the part for the second cut. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 40)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? SecondCutLength { get; set; }

    /// <summary>
    /// Width of the part for the second cut. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 41)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? SecondCutWidth { get; set; }

    /// <summary>
    /// Production route the part follows through the workstations. Optional.
    /// </summary>
    [JsonProperty(Order = 42)]
    [StringLength(255)]
    public string? ProductionRoute { get; set; }

    /// <summary>
    /// Type of the production order the part belongs to. Optional.
    /// </summary>
    [JsonProperty(Order = 43)]
    public string? ProductionOrderType { get; set; }

    /// <summary>
    /// Name of the grain matching template applied to the part. Optional.
    /// </summary>
    [JsonProperty(Order = 44)]
    [StringLength(50)]
    public string? Template { get; set; }

    /// <summary>
    /// Layout used to print the label of the part. Optional.
    /// </summary>
    [JsonProperty(Order = 45)]
    [StringLength(50)]
    public string? LabelLayout { get; set; }

    /// <summary>
    /// Lots that contain the part. Optional.
    /// </summary>
    [JsonProperty(Order = 47)]
    public ProductionOrderLotReference[]? Lots { get; set; }

    #endregion

    #region (30) IEdgebandingProperties

    /// <inheritdoc />
    [JsonProperty(Order = 32)]
    [StringLength(100)]
    public string? EdgeBack { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 35)]
    public string? EdgeDiagram { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 31)]
    [StringLength(100)]
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 33)]
    [StringLength(100)]
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 34)]
    [StringLength(100)]
    public string? EdgeRight { get; set; }

    /// <summary>
    /// Thickness of the edge on the back side. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 35)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? EdgeThicknessBack { get; set; }

    /// <summary>
    /// Thickness of the edge on the front side. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 36)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? EdgeThicknessFront { get; set; }

    /// <summary>
    /// Thickness of the edge on the left side. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 37)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? EdgeThicknessLeft { get; set; }

    /// <summary>
    /// Thickness of the edge on the right side. Optional.
    /// <para>Unit for <see cref="UnitSystem.Metric" />: mm.</para>
    /// </summary>
    [JsonProperty(Order = 38)]
    [Range(0.1, 9999.9)]
    [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
    public double? EdgeThicknessRight { get; set; }

    #endregion

    #region (40) ICncProgramProperties

    /// <summary>
    /// Name of the first CNC program applied to the part. Optional.
    /// </summary>
    [JsonProperty(Order = 40)]
    [StringLength(255)]
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Name of the second CNC program applied to the part. Optional.
    /// </summary>
    [JsonProperty(Order = 41)]
    [StringLength(255)]
    public string? CncProgramName2 { get; set; }

    /// <summary>
    /// Name of the third CNC program applied to the part. Optional.
    /// </summary>
    [JsonProperty(Order = 42)]
    [StringLength(255)]
    public string? CncProgramName3 { get; set; }

    #endregion

    #region (50) ILaminatingProperties

    /// <inheritdoc />
    [JsonProperty(Order = 51)]
    [StringLength(100)]
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Material code of the laminate applied on the bottom side. Optional.
    /// </summary>
    [JsonProperty(Order = 52)]
    [StringLength(100)]
    public string? LaminateBottom { get; set; }

    /// <summary>
    /// Grain direction of the laminate on the bottom side. Optional.
    /// </summary>
    [JsonProperty(Order = 53)]
    public Grain? LaminateBottomGrain { get; set; }

    /// <summary>
    /// Grain direction of the laminate on the top side. Optional.
    /// </summary>
    [JsonProperty(Order = 53)]
    public Grain? LaminateTopGrain { get; set; }

    #endregion
}