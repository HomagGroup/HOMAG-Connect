using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

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
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(Unknown))]
        Unknown,

        /// <summary>
        /// Ready for production
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(ReadyForProduction))]
        ReadyForProduction,

        /// <summary>
        /// In Production
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(InProduction))] 
        InProduction,

        /// <summary>
        /// Completed
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(Completed))]
        Completed,

        /// <summary>
        /// Item has been placed in the sorting shelf
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(PlacedInSortingShelf))]
        PlacedInSortingShelf,

        /// <summary>
        /// Item has been picked from the sorting shelf
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(PickedFromSortingShelf))]
        PickedFromSortingShelf,

        /// <summary>
        /// Item has been confirmed for dividing after processing at the dividing workstation
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(DividingPartConfirmed))]    
        DividingPartConfirmed,

        /// <summary>
        /// Item has been labeled
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(Labeled))]
        Labeled,

        /// <summary>
        /// Indicates whether the feedback has been confirmed.
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(FeedbackConfirmed))]
        FeedbackConfirmed,

        /// <summary>
        /// Indicates that the feedback has been confirmed by the parent item, e.g. the assembly group to which a part belongs, or the production order to which a position belongs.
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(FeedbackConfirmedByParent))]    
        FeedbackConfirmedByParent,

        /// <summary>
        /// Item has been sent to productioAssist
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(SentToProductionAssist))]
        SentToProductionAssist,

        /// <summary>
        /// Item has been received by the productionManager
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(ReceivedByProductionManager))]
        ReceivedByProductionManager,

        /// <summary>
        /// Item has been sent to intelliDivide
        /// </summary>
        [Display(ResourceType = typeof(ProductionItemFeedbackActionDisplayNames), Name = nameof(SentToIntelliDivide))]
        SentToIntelliDivide
    }
}
