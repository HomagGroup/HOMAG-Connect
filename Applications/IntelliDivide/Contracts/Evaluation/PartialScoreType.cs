namespace HomagConnect.IntelliDivide.Contracts.Evaluation
{
    /// <summary>
    /// Specifies the types of partial scores used to evaluate or analyze production solutions, such as throughput,
    /// material usage, production time, and cost metrics.
    /// </summary>
    /// <remarks>Use this enumeration to select or reference specific scoring criteria when assessing or
    /// comparing production patterns or solutions. The available values represent a range of performance, efficiency,
    /// and cost-related metrics relevant to manufacturing or optimization processes. Some values may be subject to
    /// removal in future versions; refer to the documentation for updates.</remarks>
    public enum PartialScoreType
    {
        /// <summary>
        /// Plus parts
        /// </summary>
        NumberOfPlusParts,

        /// <summary>
        /// Throughput
        /// </summary>
        Throughput,

        /// <summary>
        /// Average pattern production time
        /// </summary>
        AvgCycleProductionTime,

        /// <summary>
        /// Maximum cycle production time
        /// </summary>
        MaxCycleProductionTime,

        /// <summary>
        /// Boards
        /// </summary>
        BoardsArea,

        /// <summary>
        /// NumberOfBoards
        /// </summary>
        NumberOfBoards,

        /// <summary>
        /// Percentage of offcuts
        /// </summary>
        OffcutsDifference,

        /// <summary>
        /// Offcuts used
        /// </summary>
        OffcutsUsed,

        /// <summary>
        /// Off cuts produced
        /// </summary>
        OffcutsProduced,

        /// <summary>
        /// HeadCuts
        /// </summary>
        HeadCuts,

        /// <summary>
        /// HeadCutsPlusRecuts
        /// </summary>
        HeadCutsPlusRecuts,

        /// <summary>
        /// Recuts
        /// </summary>
        Recuts,

        /// <summary>
        /// CyclesCount
        /// </summary>
        CyclesCount,

        /// <summary>
        /// Cuts
        /// </summary>
        Cuts,

        /// <summary>
        /// Production time
        /// </summary>
        ProductionTime,

        /// <summary>
        /// Percentage of scrap
        /// </summary>
        PercentageOfScrap,

        /// <summary>
        /// Percentage of waste
        /// </summary>
        PercentageOfWaste,

        /// <summary>
        /// Maximum book weight
        /// </summary>
        MaxBookWeight,

        //To be removed later
        /// <summary>
        /// Manual offcuts used
        /// </summary>
        ManualOffcutsUsed,

        //To be removed later
        /// <summary>
        /// Manual offcuts produced
        /// </summary>
        ManualOffcutsProduced,

        //To be removed later
        /// <summary>
        /// Manual offcuts sum
        /// </summary>
        ManualOffcutsSum,

        //To be removed later
        /// <summary>
        /// large offcuts sum
        /// </summary>
        LargeOffcuts,

        //To be removed later
        /// <summary>
        /// large offcuts produced
        /// </summary>
        LargeOffcutsProduced,

        //To be removed later
        /// <summary>
        /// large offcuts used
        /// </summary>
        LargeOffcutsUsed,

        //To be removed later
        /// <summary>
        /// small offcuts sum
        /// </summary>
        SmallOffcuts,

        //To be removed later
        /// <summary>
        /// small offcuts produced
        /// </summary>
        SmallOffcutsProduced,

        //To be removed later
        /// <summary>
        /// small offcuts used
        /// </summary>
        SmallOffcutsUsed,

        /// <summary>
        /// The stack count
        /// </summary>
        StackCount,

        /// <summary>
        /// The presence periods count
        /// </summary>
        PresencePeriodsCount,

        /// <summary>
        /// The presence periods duration
        /// </summary>
        PresencePeriodsDuration,

        /// <summary>
        /// The manual cycles count
        /// </summary>
        ManualCyclesCount,

        /// <summary>
        /// The average package height
        /// </summary>
        AvgPackageHeight,

        /// <summary>
        /// How often does the machine need to switch to another tool.
        /// </summary>
        ToolChanges,

        /// <summary>
        /// How many meters need to get cut.
        /// </summary>
        CuttingLength,

        /// <summary>
        /// The area of waste
        /// </summary>
        WasteArea,

        /// <summary>
        /// The area of scrap
        /// </summary>
        ScrapArea,

        /// <summary>
        /// material cost
        /// </summary>
        MaterialCost,

        /// <summary>
        /// the cost for all edges used
        /// </summary>
        EdgeCost,

        /// <summary>
        /// the total edge length used on solution
        /// </summary>
        EdgeLength,

        /// <summary>
        /// the total cost of material (boards + offcuts + edges)
        /// </summary>
        MaterialTotalCost,

        /// <summary>
        /// Number of patterns of the solution
        /// </summary>
        PatternCount,

        /// <summary>
        /// the production cost based on production time and machine rates
        /// </summary>
        ProductionCost,

        /// <summary>
        /// the total cost of the solution
        /// </summary>
        TotalCost
    }
}
