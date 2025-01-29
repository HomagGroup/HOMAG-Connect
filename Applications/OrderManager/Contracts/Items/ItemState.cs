namespace HomagConnect.OrderManager.Contracts.Items;

/// <summary>
/// Item state.
/// </summary>
public enum OrderItemState
{
    /// <summary>
    /// Unknown
    /// </summary>
    Unknown,

    /// <summary>
    /// New
    /// </summary>
    New,

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
}