using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionAssist.Contracts.Feedback.Enumerations
{
    /// <summary>
    /// 
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum WorkstationType
    { 
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        Cutting,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        Nesting,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        Storage,
        /// <summary>
        /// Used in materialAssist
        /// </summary>
        Edgebanding,
        /// <summary>
        /// 
        /// </summary>
        Drilling,
        /// <summary>
        /// 
        /// </summary>
        CNC,
        /// <summary>
        /// 
        /// </summary>
        Preassembly,

        /// <summary>
        /// Used in productionAssist: SortingBeforeAssembly
        /// </summary>
        Sorting,

        /// <summary>
        /// Used in productionAssist
        /// </summary>
        Assembly,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        Packing,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        Shipping,

        // Feedback workplaces

        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackDrilling,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackCutting,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackNesting,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackEdgebanding,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackCNC,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackInsertFittings,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackMoulding,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackSanding,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackRollerCoating,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackSprayCoating,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackLaminating,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackSorting,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackAutomaticSorting,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackPreassembly,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackAssembly,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackPacking,
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        FeedbackShipping, 
        /// <summary>
        /// Used in productionAssist
        /// </summary>
        SortingAfterCutting,
        /// <summary>
        /// Rework
        /// </summary>
        Rework
    }
}
