using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;
using System.ComponentModel;

// Note: This is preliminary code and is subject to change

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Part of an order, including its material, dimensions, edgebanding, CNC programs and production dates.
/// </summary>
/// <example>
/// { "type": "Part", "id": "PART-1001", "material": "P2_Gold_Craft_Oak_19.0", "grain": "Lengthwise", "length": 720, "width": 596, "thickness": 19, "quantity": 2, "edgeFront": "ABS_Oak_2.0", "state": "New" }
/// </example>
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
    /// Thickness of the part. Optional.
    /// </summary>
    /// <example>19</example>
    [JsonProperty(Order = 15)]
    public double? Thickness { get; set; }

    #endregion

    #region Production Properties

    /// <summary>
    /// Planned start date and time of the part. Optional.
    /// </summary>
    /// <example>2025-04-04T06:00:00+00:00</example>
    [JsonProperty(Order = 810)]
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Date and time production of the part started. Optional.
    /// </summary>
    /// <example>2025-04-04T06:15:00+00:00</example>
    [JsonProperty(Order = 815)]
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>
        /// Planned completion date and time of the part. Optional.
        /// </summary>
        /// <example>2025-04-08T15:00:00+00:00</example>
    [JsonProperty(Order = 820)]
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Date and time production of the part was completed. Optional.
    /// </summary>
    /// <example>2025-04-08T15:40:00+00:00</example>
    [JsonProperty(Order = 825)]
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Planned delivery date and time of the part. Optional.
    /// </summary>
    /// <example>2025-05-15T00:00:00+00:00</example>
    [JsonProperty(Order = 830)]
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    /// <summary>
    /// Production route used to manufacture the part. Optional.
    /// </summary>
    /// <example>Cutting-Edgebanding-Drilling</example>
    [JsonProperty(Order = 835)]
    public string? ProductionRoute { get; set; }

    #endregion

    #region ICuttingProperties Members

    /// <summary>
    /// Finished length of the part. Optional.
    /// </summary>
    /// <example>720</example>
    [JsonProperty(Order = 200)]
    public double? FinishLength { get; set; }

    /// <summary>
    /// Finished width of the part. Optional.
    /// </summary>
    /// <example>596</example>
    [JsonProperty(Order = 210)]
    public double? FinishWidth { get; set; }

    /// <summary>
    /// Label layout used to print the label of the part. Optional.
    /// </summary>
    /// <example>Standard</example>
    [JsonProperty(Order = 260)]
    public string? LabelLayout { get; set; }

    /// <summary>
    /// Template used for the part. Optional.
    /// </summary>
    /// <example>Template-01</example>
    [JsonProperty(Order = 250)]
    public string? Template { get; set; }

    /// <summary>
    /// Length of the second cut size. Optional.
    /// </summary>
    /// <example>700</example>
    [JsonProperty(Order = 220)]
    public double? SecondCutLength { get; set; }

    /// <summary>
    /// Width of the second cut size. Optional.
    /// </summary>
    /// <example>580</example>
    [JsonProperty(Order = 230)]
    public double? SecondCutWidth { get; set; }

    #endregion

    #region IMaterialProperties

    /// <summary>
    /// Material code of the part. Optional.
    /// </summary>
    /// <example>P2_Gold_Craft_Oak_19.0</example>
    [JsonProperty(Order = 100)]
    public string? Material { get; set; }

    /// <summary>
    /// Surface of the top side. Optional.
    /// </summary>
    /// <example>Melamine</example>
    [JsonProperty(Order = 120)]
    public string? SurfaceTop { get; set; }

    /// <summary>
    /// Surface of the bottom side. Optional.
    /// </summary>
    /// <example>Melamine</example>
    [JsonProperty(Order = 130)]
    public string? SurfaceBottom { get; set; }

    /// <summary>
    /// Grain direction of the part.
    /// </summary>
    /// <example>Lengthwise</example>
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
    /// Edge thickness at the front. Optional.
    /// </summary>
    /// <example>2</example>
    [JsonProperty(Order = 415)]
    public double? EdgeThicknessFront { get; set; }

    /// <summary>
    /// Edge thickness at the back. Optional.
    /// </summary>
    /// <example>2</example>
    [JsonProperty(Order = 425)]
    public double? EdgeThicknessBack { get; set; }

    /// <summary>
    /// Edge thickness at the left. Optional.
    /// </summary>
    /// <example>0.8</example>
    [JsonProperty(Order = 435)]
    public double? EdgeThicknessLeft { get; set; }

    /// <summary>
    /// Edge thickness at the right. Optional.
    /// </summary>
    /// <example>0.8</example>
    [JsonProperty(Order = 445)]
    public double? EdgeThicknessRight { get; set; }

    #endregion

    #region ICncProgramProperties

    /// <summary>
    /// Name of the first CNC program. Optional.
    /// </summary>
    /// <example>Drill_Hinges.mpr</example>
    [JsonProperty(Order = 410)]
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Name of the second CNC program. Optional.
    /// </summary>
    /// <example>Groove_Back.mpr</example>
    [JsonProperty(Order = 420)]
    public string? CncProgramName2 { get; set; }

    /// <summary>
    /// Name of the third CNC program. Optional.
    /// </summary>
    /// <example>Contour.mpr</example>
    [JsonProperty(Order = 430)]
    public string? CncProgramName3 { get; set; }

    #endregion

    #region ILaminatingProperties

    /// <inheritdoc />
    [JsonProperty(Order = 510)]
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Material code of the laminate applied on the bottom. Optional.
    /// </summary>
    /// <example>HPL_White_0.8</example>
    [JsonProperty(Order = 530)]
    public string? LaminateBottom { get; set; }

    /// <summary>
    /// Grain direction of the top laminate. Optional.
    /// </summary>
    /// <example>Lengthwise</example>
    [JsonProperty(Order = 520)]
    public Grain? LaminateTopGrain { get; set; }

    /// <summary>
    /// Grain direction of the bottom laminate. Optional.
    /// </summary>
    /// <example>None</example>
    [JsonProperty(Order = 540)]
    public Grain? LaminateBottomGrain { get; set; }

    #endregion
}