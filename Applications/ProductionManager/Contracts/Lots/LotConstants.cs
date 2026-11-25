namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Defines validation constraints for lot-related contract properties.
    /// </summary>
    public static class LotConstraints
    {
        /// <summary>
        /// Maximum number of parts allowed in a lot.
        /// </summary>
        /// <example>100000</example>
        public const int MaxQuantityOfProductionOrders = 100000;

        /// <summary>
        /// Maximum allowed length of a lot name, in characters.
        /// </summary>
        /// <example>100</example>
        public const int MaxNameLength = 100;
    }
}
