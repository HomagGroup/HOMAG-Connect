namespace HomagConnect.ProductionManager.Contracts.Events.ProductionItem;

#pragma warning disable CS8618

// Note: This is preliminary code and is subject to change

/// <summary>
/// Action that was performed on a production item. Serialized as the member name.
/// </summary>
public enum ProductionItemProcessedAction
{
    /// <summary>
    /// Action is not known.
    /// </summary>
    Unknown,

    /// <summary>
    /// The production item was sent to productionAssist.
    /// </summary>
    SentToProductionAssist,

    /// <summary>
    /// The production item was sent to intelliDivide.
    /// </summary>
    SentToIntelliDivide,

    /// <summary>
    /// The production item was cut.
    /// </summary>
    Cut
}