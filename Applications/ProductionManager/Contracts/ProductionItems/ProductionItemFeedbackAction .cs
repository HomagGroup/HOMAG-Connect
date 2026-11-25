using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems
{
    /// <summary>
    /// Action that a production item feedback reports, such as a status change or a handling step. Serialized as the member name.
    /// </summary>
    [ResourceManager(typeof(ProductionItemFeedbackActionDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ProductionItemFeedbackAction
    {
        /// <summary>
        /// The action is not known. Do not use it when reporting feedback.
        /// </summary>
        Unknown,

        /// <summary>
        /// The item has been released and is waiting to be produced.
        /// </summary>
        ReadyForProduction,

        /// <summary>
        /// Production of the item has started.
        /// </summary>
        InProduction,

        /// <summary>
        /// Production of the item is finished.
        /// </summary>
        Completed,

        /// <summary>
        /// The item was placed in the sorting shelf.
        /// </summary>
        PlacedInSortingShelf,

        /// <summary>
        /// The item was picked from the sorting shelf.
        /// </summary>
        PickedFromSortingShelf,

        /// <summary>
        /// The item was confirmed for dividing after processing at the dividing workstation.
        /// </summary>
        DividingPartConfirmed,

        /// <summary>
        /// The item was labeled.
        /// </summary>
        Labeled,

        /// <summary>
        /// The feedback for the item was confirmed.
        /// </summary>
        FeedbackConfirmed,

        /// <summary>
        /// The feedback was confirmed through the parent item, for example the assembly group a part belongs to.
        /// </summary>
        FeedbackConfirmedByParent,

        /// <summary>
        /// The item was sent to productionAssist.
        /// </summary>
        SentToProductionAssist,

        /// <summary>
        /// The item was received by productionManager.
        /// </summary>
        ReceivedByProductionManager,

        /// <summary>
        /// The item was sent to intelliDivide.
        /// </summary>
        SentToIntelliDivide
    }
}
