using System.ComponentModel.DataAnnotations;

using HomagConnect.ProductionManager.Contracts.Lots;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems
{
    /// <summary>
    /// Reference from a production item to a lot, including how many pieces of the item are in that lot.
    /// </summary>
    /// <example>
    /// { "lotId": "9c1e7a52-3b64-4f08-a1d2-5e6f7a8b9c0d", "lotName": "Lot 2025-04-02", "quantity": 2 }
    /// </example>
    public class ProductionOrderLotReference
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionOrderLotReference" /> class.
        /// </summary>
        public ProductionOrderLotReference() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionOrderLotReference" /> class.
        /// </summary>
        public ProductionOrderLotReference(Guid lotId, string? lotName, int quantity)
        {
            LotId = lotId;
            LotName = lotName;
            Quantity = quantity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionOrderLotReference" /> class.
        /// </summary>
        public ProductionOrderLotReference(Guid lotId, int quantity) : this(lotId, null, quantity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionOrderLotReference" /> class.
        /// </summary>
        public ProductionOrderLotReference(Lot lot, int quantity)
        {
            LotId = lot.Id;
            LotName = lot.Name;
            Quantity = quantity;
        }

        #endregion

        /// <summary>
        /// Unique identifier of the lot.
        /// </summary>
        /// <example>9c1e7a52-3b64-4f08-a1d2-5e6f7a8b9c0d</example>
        [JsonProperty(Order = 1)]
        public Guid LotId { get; set; }

        /// <summary>
        /// Name of the lot. Optional.
        /// </summary>
        /// <example>Lot 2025-04-02</example>
        [JsonProperty(Order = 2)]
        public string? LotName { get; set; }

        /// <summary>
        /// Number of pieces of the production item that are in the lot.
        /// </summary>
        /// <example>2</example>
        [JsonProperty(Order = 3)]
        [Range(1, LotConstraints.MaxQuantityOfProductionOrders)]
        public int Quantity { get; set; }
    }
}