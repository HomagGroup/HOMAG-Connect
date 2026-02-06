using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result;

/// <summary>
/// Identifies measurable key figures used for scoring solution candidates.
/// Each enum member can be decorated with <see cref="LowerIsBetterAttribute" /> or <see cref="HigherIsBetterAttribute" />
/// indicating the desired optimization direction for normalization and scoring.
/// </summary>
[JsonConverter(typeof(TolerantEnumConverter))]
public enum SolutionKeyFigure
{
    /// <summary>
    /// Unknown or unspecified key figure.
    /// </summary>
    Unknown = 0,

    #region Material

    /// <summary>
    /// Total waste area; lower values are better.
    /// </summary>
    [LowerIsBetter]
    WasteArea,

    /// <summary>
    /// Total waste plus offcuts area; lower values are better.
    /// </summary>
    [LowerIsBetter]
    WastePlusOffcutsArea,

    /// <summary>
    /// Waste as percentage of total; lower values are better.
    /// </summary>
    [LowerIsBetter]
    WastePercentage,

    [LowerIsBetter]
    WastePlusOffcutsPercentage,

    #region Offcuts

    /// <summary>
    /// Number of small offcuts produced; lower values are better.
    /// </summary>
    [LowerIsBetter]
    OffcutsSmallProduced,

    /// <summary>
    /// Number of small offcuts used (recycled); higher values are better.
    /// </summary>
    [HigherIsBetter]
    OffcutsSmallRequired,

    /// <summary>
    /// Number of small offcuts total; lower values are better.
    /// </summary>
    [LowerIsBetter]
    OffcutsSmallTotal,

    /// <summary>
    /// Number of large offcuts produced; lower values are better.
    /// </summary>
    [LowerIsBetter]
    OffcutsLargeProduced,

    /// <summary>
    /// Number of large offcuts used (recycled); higher values are better.
    /// </summary>
    [HigherIsBetter]
    OffcutsLargeRequired,

    /// <summary>
    /// Total large offcuts; lower values are better.
    /// </summary>
    [LowerIsBetter]
    OffcutsLargeTotal,

    /// <summary>
    /// Total offcuts produced; lower values are better.
    /// </summary>
    [LowerIsBetter]
    OffcutsProduced,

    /// <summary>
    /// Indicates the number of offcuts required as a metric for evaluation.
    /// </summary>
    /// <remarks>
    /// A higher value for this metric is considered better when comparing results, as
    /// indicated by the HigherIsBetter attribute.
    /// </remarks>
    [HigherIsBetter]
    OffcutsRequired,

    /// <summary>
    /// Total offcuts (produced minus required); lower values are better.
    /// </summary>
    [LowerIsBetter]
    OffcutsTotal,

    #endregion

    #region Boards

    /// <summary>
    /// Required whole boards count; lower values are better.
    /// </summary>
    [LowerIsBetter]
    WholeBoardsRequired,

    #endregion

    #region Edgebands

    /// <summary>
    /// Total edge banding length; lower values are better.
    /// </summary>
    [LowerIsBetter]
    EdgebandLength,

    #endregion

    #endregion

    #region Production

    /// <summary>
    /// Total production time; lower values are better.
    /// </summary>
    [LowerIsBetter]
    ProductionTime,

    /// <summary>
    /// Production time per part; lower values are better.
    /// </summary>
    [LowerIsBetter]
    ProductionTimePerPart,

    /// <summary>
    /// Book height average; higher values are better.
    /// </summary>
    [HigherIsBetter]
    BookHeightAverage,

    /// <summary>
    /// Book height maximum; higher values are better.
    /// </summary>
    [HigherIsBetter]
    BookHeightMax,

    #region Parts

    /// <summary>
    /// Quantity of produced parts; higher values are better.
    /// </summary>
    [HigherIsBetter]
    PartsQuantity,

    /// <summary>
    /// Quantity of plus parts; higher values are better.
    /// </summary>
    [HigherIsBetter]
    PartsQuantityPlusParts,

    /// <summary>
    /// Total parts quantity (including plus parts); higher values are better.
    /// </summary>
    [HigherIsBetter]
    PartsQuantityTotal,

    /// <summary>
    /// Quantity produced in manual mode; lower values are better.
    /// </summary>
    [LowerIsBetter]
    PartsQuantityManualMode,

    /// <summary>
    /// Quantity produced in automatic mode; higher values are better.
    /// </summary>
    [HigherIsBetter]
    PartsQuantityAutomaticMode,

    #endregion

    #region Handling

    /// <summary>
    /// Book weight average; lower values are better.
    /// </summary>
    [LowerIsBetter]
    BookWeightAverage,

    /// <summary>
    /// Book weight maximum; lower values are better.
    /// </summary>
    [LowerIsBetter]
    BookWeightMax,

    /// <summary>
    /// Number of cycles; lower values are better.
    /// </summary>
    [LowerIsBetter]
    Cycles,

   
    /// <summary>
    /// Number of cutting patterns; lower values are better.
    /// </summary>
    [LowerIsBetter]
    PatternCount,

    /// <summary>
    /// Number of cuts; lower values are better.
    /// </summary>
    [LowerIsBetter]
    Cuts,

    /// <summary>
    /// Number of recuts; lower values are better.
    /// </summary>
    [LowerIsBetter]
    Recuts,

    /// <summary>
    /// Number of headcuts; lower values are better.
    /// </summary>
    [LowerIsBetter]
    Headcuts,

    /// <summary>
    /// Total cutting length; lower values are better.
    /// </summary>
    [LowerIsBetter]
    CuttingLength,

    [HigherIsBetter]
    QuantityPerPatternAverage,

    #endregion

    #endregion

    #region Costs

    /// <summary>
    /// Total costs; lower values are better.
    /// </summary>
    [LowerIsBetter]
    TotalCosts,

    /// <summary>
    /// Total costs per part; lower values are better.
    /// </summary>
    [LowerIsBetter]
    TotalCostsPerPart,

    /// <summary>
    /// Material costs; lower values are better.
    /// </summary>
    [LowerIsBetter]
    MaterialCosts,

    /// <summary>
    /// Material costs per part; lower values are better.
    /// </summary>
    [LowerIsBetter]
    MaterialCostsPerPart,

    /// <summary>
    /// Production costs; lower values are better.
    /// </summary>
    [LowerIsBetter]
    ProductionCosts,

    /// <summary>
    /// Production costs per part; lower values are better.
    /// </summary>
    [LowerIsBetter]
    ProductionCostsPerPart,

    /// <summary>
    /// Combined boards and offcuts costs; lower values are better.
    /// </summary>
    [LowerIsBetter]
    BoardsPlusOffcutsCosts,

    /// <summary>
    /// Edgeband costs; lower values are better.
    /// </summary>
    [LowerIsBetter]
    EdgebandCosts,

    #endregion
  
}