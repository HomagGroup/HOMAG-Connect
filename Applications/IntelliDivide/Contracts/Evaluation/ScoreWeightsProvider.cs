using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Evaluation
{
    /// <summary>
    /// Provides methods for retrieving predefined sets of weights for partial score types used in scoring calculations.
    /// </summary>
    /// <remarks>Use the methods of this class to obtain weight dictionaries tailored to different scoring
    /// scenarios, such as balancing material cost and scrap area or emphasizing scrap percentage. This class is static
    /// and cannot be instantiated.</remarks>
    public static class ScoreWeightsProvider
    {
        private const int _AccentuatedWeight = 3000;

        /// <summary>
        /// Returns a dictionary of balanced weights for each partial score type, adjusted based on whether material
        /// cost is considered.
        /// </summary>
        /// <remarks>When hasMaterialCost is false, the weight for MaterialCost is set to 0 and the weight
        /// for ScrapArea is increased to 800. This allows callers to control whether material cost or scrap area is
        /// prioritized in the weighting scheme.</remarks>
        /// <param name="hasMaterialCost">true to include material cost in the weighting; false to exclude material cost and emphasize scrap area
        /// instead.</param>
        /// <returns>A dictionary mapping each PartialScoreType to its corresponding weight. The weights reflect the relative
        /// importance of each score type for balancing purposes.</returns>
        public static IDictionary<PartialScoreType, int> GetBalancedWeights(bool hasMaterialCost)
        {
            var balanceWeights = new Dictionary<PartialScoreType, int>
            {
                [PartialScoreType.MaterialCost] = 800,
                [PartialScoreType.ProductionTime] = 800,
                [PartialScoreType.WasteArea] = 800,
                [PartialScoreType.PresencePeriodsDuration] = 200,
                [PartialScoreType.ManualCyclesCount] = 200,
                [PartialScoreType.StackCount] = 200,
                [PartialScoreType.OffcutsDifference] = 100,
                [PartialScoreType.ManualOffcutsSum] = 100,
                [PartialScoreType.PresencePeriodsCount] = 100,
                [PartialScoreType.AvgCycleProductionTime] = 50,
                [PartialScoreType.BoardsArea] = 50,
                [PartialScoreType.NumberOfBoards] = 50,
                [PartialScoreType.Cuts] = 50,
                [PartialScoreType.HeadCuts] = 50,
                [PartialScoreType.MaxBookWeight] = 50,
                [PartialScoreType.MaxCycleProductionTime] = 50,
                [PartialScoreType.CyclesCount] = 50,
                [PartialScoreType.Recuts] = 50,

                [PartialScoreType.ToolChanges] = 0,
                [PartialScoreType.CuttingLength] = 200,

                [PartialScoreType.PatternCount] = 0,
                [PartialScoreType.NumberOfPlusParts] = 0,
                [PartialScoreType.HeadCutsPlusRecuts] = 0,
                [PartialScoreType.Throughput] = 0,
                [PartialScoreType.AvgPackageHeight] = 0,
                [PartialScoreType.PercentageOfScrap] = 0,
                [PartialScoreType.PercentageOfWaste] = 0,
                [PartialScoreType.ScrapArea] = 0,

                [PartialScoreType.EdgeLength] = 0,
                [PartialScoreType.EdgeCost] = 0,
                [PartialScoreType.MaterialTotalCost] = 0,

                [PartialScoreType.OffcutsProduced] = 0,
                [PartialScoreType.OffcutsUsed] = 0,
                [PartialScoreType.ProductionCost] = 0,

            };

            if (hasMaterialCost)
            {
                return balanceWeights;
            }

            balanceWeights[PartialScoreType.ScrapArea] = 800;
            balanceWeights[PartialScoreType.MaterialCost] = 0;

            return balanceWeights;
        }

        /// <summary>
        /// Returns a dictionary of partial score weights with the weight for scrap percentage accentuated.
        /// </summary>
        /// <remarks>Use this method when the scrap percentage should have a greater influence on the
        /// overall score compared to other factors.</remarks>
        /// <param name="hasMaterialCost">true to include material cost in the weight calculation; otherwise, false.</param>
        /// <returns>A dictionary mapping each partial score type to its corresponding weight, with the scrap percentage weight
        /// significantly increased.</returns>
        public static IDictionary<PartialScoreType, int> GetScrapAccentuatedWeights(bool hasMaterialCost)
        {
            var weights = GetBalancedWeights(hasMaterialCost);
            weights[PartialScoreType.PercentageOfScrap] = _AccentuatedWeight * 10;
            return weights;
        }
    }
}
