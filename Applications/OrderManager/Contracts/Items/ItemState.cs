namespace HomagConnect.OrderManager.Contracts.Items;

/// <summary>
/// Item state.
/// </summary>
public enum ItemState
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