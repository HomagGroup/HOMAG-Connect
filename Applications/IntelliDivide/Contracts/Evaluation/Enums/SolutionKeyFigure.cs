using HomagConnect.Base.Contracts.Converter;
using HomagConnect.IntelliDivide.Contracts.Evaluation.Attributes;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation.Enums
{
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

        #region Material key figures

        /// <summary>
        /// Total waste area; lower values are better.
        /// </summary>
        [LowerIsBetter]
        WasteArea,

        /// <summary>
        /// Waste as percentage of total; lower values are better.
        /// </summary>
        [LowerIsBetter]
        WastePercentage,

        /// <summary>
        /// Combined waste and offcut area; lower values are better.
        /// </summary>
        [LowerIsBetter]
        WasteAndOffcutArea,

        /// <summary>
        /// Combined waste and offcut percentage; lower values are better.
        /// </summary>
        [LowerIsBetter]
        WasteAndOffcutPercentage,

        /// <summary>
        /// Number of small offcuts produced; lower values are better.
        /// </summary>
        [LowerIsBetter]
        OffcutsSmallProduced,

        /// <summary>
        /// Number of small offcuts used (recycled); higher values are better.
        /// </summary>
        [HigherIsBetter]
        OffcutsSmallUsed,

        /// <summary>
        /// Total small offcuts; lower values are better.
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
        OffcutsLargeUsed,

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
        /// Total offcuts used (recycled); higher values are better.
        /// </summary>
        [HigherIsBetter]
        OffcutsUsed,

        /// <summary>
        /// Sum of produced offcuts minus used/recycled; lower values are better.
        /// </summary>
        [LowerIsBetter]
        OffcutsTotal,

        /// <summary>
        /// Required whole boards count; lower values are better.
        /// </summary>
        [LowerIsBetter]
        WholeBoardsRequired,

        #endregion

        #region Production key figures

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

        /// <summary>
        /// Number of cycles; lower values are better.
        /// </summary>
        [LowerIsBetter]
        CycleCount,

        /// <summary>
        /// Number of cutting patterns; lower values are better.
        /// </summary>
        [LowerIsBetter]
        PatternCount,

        /// <summary>
        /// Average book height; higher values are better.
        /// </summary>
        [HigherIsBetter]
        AverageBookHeight,

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

        #endregion

        #region Cost key figures

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
        BoardsAndOffcutsCosts,

        /// <summary>
        /// Edgeband costs; lower values are better.
        /// </summary>
        [LowerIsBetter]
        EdgebandCosts

        #endregion
    }
}