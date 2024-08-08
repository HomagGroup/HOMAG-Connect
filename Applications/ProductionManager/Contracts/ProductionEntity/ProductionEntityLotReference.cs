namespace HomagConnect.ProductionManager.Contracts.ProductionEntity
{
    /// <summary>
    /// Production entity lot reference.
    /// </summary>
    public class ProductionEntityLotReference
    {
        /// <summary>
        /// Gets or sets the lot id.
        /// </summary>
        public Guid LotId { get; set; }

        /// <summary>
        /// Gets or sets the lot name.
        /// </summary>
        public string? LotName { get; set; }

        /// <summary>
        /// Gets or sets the quantity of entities in the lot.
        /// </summary>
        public int Quantity { get; set; }
    }
}