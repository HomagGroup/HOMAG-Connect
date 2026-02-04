using HomagConnect.Base.Contracts.Converter;
using HomagConnect.IntelliDivide.Contracts.Evaluation.Attributes;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation.Enums
{
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum SolutionKeyFigure
    {
        Unknown = 0,

        // Material
        [LowerIsBetter]
        WasteArea,

        [LowerIsBetter]
        WastePercentage,

        [LowerIsBetter]
        WasteAndOffcutArea,

        [LowerIsBetter]
        WasteAndOffcutPercentage,

        [LowerIsBetter]
        OffcutsSmallProduced,

        [HigherIsBetter]
        OffcutsSmallUsed,

        [LowerIsBetter]
        OffcutsSmallTotal,

        [LowerIsBetter]
        OffcutsLargeProduced,

        [HigherIsBetter]
        OffcutsLargeUsed,

        [LowerIsBetter]
        OffcutsLargeTotal,

        [LowerIsBetter]
        OffcutsProduced,

        [HigherIsBetter]
        OffcutsUsed,

        [LowerIsBetter]
        OffcutsTotal,

        [LowerIsBetter]
        WholeBoardsRequired,

        // Production
        [LowerIsBetter]
        ProductionTime,

        [LowerIsBetter]
        ProductionTimePerPart,

        [HigherIsBetter]
        PartsQuantity,

        [HigherIsBetter]
        PartsQuantityPlusParts,

        [HigherIsBetter]
        PartsQuantityTotal,

        [LowerIsBetter]
        PartsQuantityManualMode,

        [HigherIsBetter]
        PartsQuantityAutomaticMode,

        [LowerIsBetter]
        CycleCount,

        [LowerIsBetter]
        PatternCount,

        [HigherIsBetter]
        AverageBookHeight,

        [LowerIsBetter]
        Cuts,

        [LowerIsBetter]
        Recuts,

        [LowerIsBetter]
        Headcuts,

        [LowerIsBetter]
        CuttingLength,

        // Costs
        [LowerIsBetter]
        TotalCosts,

        [LowerIsBetter]
        TotalCostsPerPart,

        [LowerIsBetter]
        MaterialCosts,

        [LowerIsBetter]
        MaterialCostsPerPart,

        [LowerIsBetter]
        ProductionCosts,

        [LowerIsBetter]
        ProductionCostsPerPart,

        [LowerIsBetter]
        BoardsAndOffcutsCosts,

        [LowerIsBetter]
        EdgebandCosts
    }
}