using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Order item part
/// </summary>
public class Part : ComponentBase, ILaminatingProperties, IEdgebandingProperties, IDimensionProperties,
    IMaterialProperties, ICncProgramProperties, ICuttingProperties
{
    #region ComponentBase Members

    /// <inheritdoc cref="Base" />
    public override Type Type
    {
        get
        {
            return Type.Part;
        }
        set
        {
            // Ignore
        }
    }

    #endregion

    #region IDimensionProperties

    /// <summary>
    /// Gets or sets the thickness.
    /// </summary>
    [JsonProperty(Order = 15)]
    public double? Thickness { get; set; }

    #endregion

    #region Production Properties

    /// <summary>
    /// Gets or sets the planned end date of the  entity.
    /// </summary>
    [JsonProperty(Order = 810)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the started at date of the  entity.
    /// </summary>
    [JsonProperty(Order = 815)]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>Gets or sets the completed date planned</summary>
    [JsonProperty(Order = 820)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the completed at date of the  entity.
    /// </summary>
    [JsonProperty(Order = 825)]
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned delivery date of the  entity.
    /// </summary>
    [JsonProperty(Order = 830)]
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    #endregion

    #region ICuttingProperties Members

    /// <summary>
    /// Finish length
    /// </summary>
    [JsonProperty(Order = 200)]
    public double? FinishLength { get; set; }

    /// <summary>
    /// Finish width
    /// </summary>
    [JsonProperty(Order = 210)]
    public double? FinishWidth { get; set; }

    /// <summary>
    /// Label layout
    /// </summary>
    [JsonProperty(Order = 260)]
    public string? LabelLayout { get; set; }

    /// <summary>
    /// Template
    /// </summary>
    [JsonProperty(Order = 250)]
    public string? Template { get; set; }

    /// <summary>
    /// 2. Cut size length
    /// </summary>
    [JsonProperty(Order = 220)]
    public double? SecondCutLength { get; set; }

    /// <summary>
    /// 2. Cut size width
    /// </summary>
    [JsonProperty(Order = 230)]
    public double? SecondCutWidth { get; set; }

    #endregion

    #region IMaterialProperties

    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    [JsonProperty(Order = 100)]
    public string? Material { get; set; }

    /// <summary>
    /// Surface Top
    /// </summary>
    [JsonProperty(Order = 120)]
    public string? SurfaceTop { get; set; }

    /// <summary>
    /// Surface Bottom
    /// </summary>
    [JsonProperty(Order = 130)]
    public string? SurfaceBottom { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    [JsonProperty(Order = 110)]
    public Grain Grain { get; set; }

    #endregion

    #region IEdgebandingProperties

    /// <inheritdoc />
    [JsonProperty(Order = 410)]
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 420)]
    public string? EdgeBack { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 450)]
    public string? EdgeDiagram { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 430)]
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    [JsonProperty(Order = 440)]
    public string? EdgeRight { get; set; }

    /// <summary>
    /// Edge Thickness Front
    /// </summary>
    [JsonProperty(Order = 415)]
    public double? EdgeThicknessFront { get; set; }

    /// <summary>
    /// Edge Thickness Back
    /// </summary>
    [JsonProperty(Order = 425)]
    public double? EdgeThicknessBack { get; set; }

    /// <summary>
    /// Edge Thickness Left
    /// </summary>
    [JsonProperty(Order = 435)]
    public double? EdgeThicknessLeft { get; set; }

    /// <summary>
    /// Edge Thickness Right
    /// </summary>
    [JsonProperty(Order = 445)]
    public double? EdgeThicknessRight { get; set; }

    #endregion

    #region ICncProgramProperties

    /// <summary>
    /// Cnc program name 1
    /// </summary>
    [JsonProperty(Order = 410)]
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Cnc program name 2
    /// </summary>
    [JsonProperty(Order = 420)]
    public string? CncProgramName2 { get; set; }

    /// <summary>
    /// CNC Program Name 3
    /// </summary>
    [JsonProperty(Order = 430)]
    public string? CncProgramName3 { get; set; }

    #endregion

    #region ILaminatingProperties

    /// <inheritdoc />
    [JsonProperty(Order = 510)]
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Gets or sets the material code of the laminate type which should get applied on the bottom.
    /// </summary>
    [JsonProperty(Order = 530)]
    public string? LaminateBottom { get; set; }

    /// <summary>
    /// LaminateTopGrain: optional
    /// </summary>
    [JsonProperty(Order = 520)]
    public Grain? LaminateTopGrain { get; set; }

    /// <summary>
    /// LaminateBottomGrain: optional
    /// </summary>
    [JsonProperty(Order = 540)]
    public Grain? LaminateBottomGrain { get; set; }

    #endregion
}