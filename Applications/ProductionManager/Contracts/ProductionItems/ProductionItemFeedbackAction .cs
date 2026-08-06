using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems
{
    /// <summary>
    /// 
    /// </summary>
    [ResourceManager(typeof(ProductionItemFeedbackActionDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ProductionItemFeedbackAction
    {
        /// <summary>
        /// Default unknown value
        /// </summary>
        Unknown,

        /// <summary>
        /// Ready for production
        /// </summary>
        ReadyForProduction,

        /// <summary>
        /// In Production
        /// </summary>
        InProduction,

        /// <summary>
        /// Completed
        /// </summary>
        Completed,

        /// <summary>
        /// Item has been placed in the sorting shelf
        /// </summary>
        PlacedInSortingShelf,

        /// <summary>
        /// Item has been picked from the sorting shelf
        /// </summary>
        PickedFromSortingShelf,

        /// <summary>
        /// Item has been confirmed for dividing after processing at the dividing workstation
        /// </summary>
        DividingPartConfirmed,

        /// <summary>
        /// Item has been labeled
        /// </summary>
        Labeled,

        /// <summary>
        /// Indicates whether the feedback has been confirmed.
        /// </summary>
        FeedbackConfirmed,

        /// <summary>
        /// Indicates that the feedback has been confirmed by the parent item, e.g. the assembly group to which a part belongs, or the production order to which a position belongs.
        /// </summary>
        FeedbackConfirmedByParent,

        /// <summary>
        /// Item has been sent to productioAssist
        /// </summary>
        SentToProductionAssist,

        /// <summary>
        /// Item has been received by the productionManager
        /// </summary>
        ReceivedByProductionManager,

        /// <summary>
        /// Item has been sent to intelliDivide
        /// </summary>
        SentToIntelliDivide
    }
}
