using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.OrderManager.Contracts.Items;

public class ItemPart : Component, ILaminatingProperties, IEdgebandingProperties, IDimensionProperties, IMaterialProperties, ICncProgramProperties, ICuttingProperties
{
    /// <summary>
    /// Grouping
    /// </summary>
    public string? Grouping { get; set; }

    #region ICuttingProperties Members

    /// <summary>
    /// Finish length
    /// </summary>
    public decimal? FinishLength { get; set; }

    /// <summary>
    /// Finish width
    /// </summary>
    public decimal? FinishWidth { get; set; }

    /// <summary>
    /// Label layout
    /// </summary>
    public string? LabelLayout { get; set; }

    /// <summary>
    /// Template
    /// </summary>
    public string? Template { get; set; }

    /// <summary>
    /// 2. Cut size length
    /// </summary>
    public decimal? SecondCutLength { get; set; }

    /// <summary>
    /// 2. Cut size width
    /// </summary>
    public decimal? SecondCutWidth { get; set; }

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
    public decimal? EdgeThicknessFront { get; set; }

    /// <summary>
    /// Edge Thickness Back
    /// </summary>
    public decimal? EdgeThicknessBack { get; set; }

    /// <summary>
    /// Edge Thickness Left
    /// </summary>
    public decimal? EdgeThicknessLeft { get; set; }

    /// <summary>
    /// Edge Thickness Right
    /// </summary>
    public decimal? EdgeThicknessRight { get; set; }

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

    /// <summary>
    /// Surface Top
    /// </summary>
    public string? SurfaceTop { get; set; }

    /// <summary>
    /// Surface Bottom
    /// </summary>
    public string? SurfaceBottom { get; set; }

    #endregion
}