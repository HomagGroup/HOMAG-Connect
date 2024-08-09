namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Constants for lots.
    /// </summary>
    public static class LotConstraints
    {
        /// <summary>
        /// Gets the maximum quantity of parts in a lot.
        /// </summary>
        public const int MaxQuantityOfProductionOrders = 100000;

        /// <summary>
        /// Gets the maximum length of a lot name.
        /// </summary>
        public const int MaxNameLength = 100;
    }
}