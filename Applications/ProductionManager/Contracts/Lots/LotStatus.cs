using HomagConnect.ProductionManager.Contracts.ProductionItems;

namespace HomagConnect.ProductionManager.Contracts.Lots;

/// <summary>
/// Enumeration of possible statuses of a <see cref="Lot" />.
/// </summary>
public enum LotStatus
{
    /// <summary>
    /// The status of a lot is "New" when it is created.
    /// </summary>
    New,

    /// <summary>
    /// After a lot has been sent to the optimization it receives the status "ReadyForProduction".
    /// </summary>
    ReadyForProduction,

    /// <summary>
    /// Once the first <see cref="ProductionEntity" /> in a <see cref="Lot" />> achieves the status
    /// <see cref="ProductionItemStatus.InProduction" />, the lot will also be marked as
    /// <see cref="LotStatus.InProduction" />.
    /// </summary>
    InProduction,

    /// <summary>
    /// After all <see cref="ProductionEntity" /> in a <see cref="Lot" /> have achieved the status
    /// <see cref="ProductionItemStatus.Completed" />, the lot will also be marked as
    /// <see cref="LotStatus.Completed" />.
    /// </summary>
    Completed
}