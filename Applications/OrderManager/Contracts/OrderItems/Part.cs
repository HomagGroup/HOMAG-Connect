using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.OrderManager.Contracts.OrderItems;

/// <summary>
/// Order item part
/// </summary>
public class Part : Component, ILaminatingProperties, IEdgebandingProperties, IDimensionProperties, IMaterialProperties, ICncProgramProperties, ICuttingProperties
{
    /// <summary>
    /// Gets or sets the article number.
    /// </summary>
    public string? ArticleNumber { get; set; }

    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the group.
    /// </summary>
    public string? ArticleGroup { get; set; }

    /// <summary>
    /// Gets or sets the state of the entity.
    /// </summary>
    public State State { get; set; } = State.New;

    /// <summary>
    /// Barcode used to identify a production entity.
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the production entity.
    /// </summary>
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the length.
    /// </summary>
    public double? Length { get; set; }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public double? Width { get; set; }

    /// <summary>
    /// Gets or sets the planned end date of the  entity.
    /// </summary>
    public DateTimeOffset? StartDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the started at date of the  entity.
    /// </summary>
    public DateTimeOffset? StartedAt { get; set; }

    /// <summary>Gets or sets the completed date planned</summary>
    public DateTimeOffset? CompletionDatePlanned { get; set; }

    /// <summary>
    /// Gets or sets the completed at date of the  entity.
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned delivery date of the  entity.
    /// </summary>
    public DateTimeOffset? DeliveryDatePlanned { get; set; }

    /// <summary>
    /// Surface Top
    /// </summary>
    public string? SurfaceTop { get; set; }

    /// <summary>
    /// Surface Bottom
    /// </summary>
    public string? SurfaceBottom { get; set; }

    #region ICuttingProperties Members

    /// <summary>
    /// Finish length
    /// </summary>
    public double? FinishLength { get; set; }

    /// <summary>
    /// Finish width
    /// </summary>
    public double? FinishWidth { get; set; }

    /// <summary>
    /// Label layout
    /// </summary>
    public double? LabelLayout { get; set; }

    /// <summary>
    /// Template
    /// </summary>
    public string? Template { get; set; }

    /// <summary>
    /// 2. Cut size length
    /// </summary>
    public double? SecondCutLength { get; set; }

    /// <summary>
    /// 2. Cut size width
    /// </summary>
    public double? SecondCutWidth { get; set; }

    #endregion

    #region IMaterialProperties

    /// <summary>
    /// Gets or sets the material.
    /// </summary>
    public string? Material { get; set; }

    /// <summary>
    /// Gets or sets the grain.
    /// </summary>
    public Grain Grain { get; set; }

    #endregion

    #region (30) IEdgebandingProperties

    /// <inheritdoc />
    public string? EdgeBack { get; set; }

    /// <inheritdoc />
    public string? EdgeDiagram { get; set; }

    /// <inheritdoc />
    public string? EdgeFront { get; set; }

    /// <inheritdoc />
    public string? EdgeLeft { get; set; }

    /// <inheritdoc />
    public string? EdgeRight { get; set; }

    /// <summary>
    /// Edge Thickness Front
    /// </summary>
    public double? EdgeThicknessFront { get; set; }

    /// <summary>
    /// Edge Thickness Back
    /// </summary>
    public double? EdgeThicknessBack { get; set; }

    /// <summary>
    /// Edge Thickness Left
    /// </summary>
    public double? EdgeThicknessLeft { get; set; }

    /// <summary>
    /// Edge Thickness Right
    /// </summary>
    public double? EdgeThicknessRight { get; set; }

    #endregion

    #region (40) ICncProgramProperties

    /// <summary>
    /// Cnc program name 1
    /// </summary>
    public string? CncProgramName1 { get; set; }

    /// <summary>
    /// Cnc program name 2
    /// </summary>
    public string? CncProgramName2 { get; set; }

    /// <summary>
    /// CNC Program Name 3
    /// </summary>
    public string? CncProgramName3 { get; set; }

    #endregion

    #region (50) ILaminatingProperties

    /// <inheritdoc />
    public string? LaminateTop { get; set; }

    /// <summary>
    /// Gets or sets the material code of the laminate type which should get applied on the bottom.
    /// </summary>
    public string? LaminateBottom { get; set; }

    /// <summary>
    /// LaminateTopGrain: optional
    /// </summary>
    public Grain? LaminateTopGrain { get; set; }

    /// <summary>
    /// LaminateBottomGrain: optional
    /// </summary>
    public Grain? LaminateBottomGrain { get; set; }

    #endregion
}