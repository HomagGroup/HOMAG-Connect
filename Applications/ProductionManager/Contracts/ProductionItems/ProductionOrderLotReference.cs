using System.ComponentModel.DataAnnotations;

using HomagConnect.ProductionManager.Contracts.Lots;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.ProductionItems
{
    /// <summary>
    /// Production entity lot reference.
    /// </summary>
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
        /// Gets or sets the lot id.
        /// </summary>
        [JsonProperty(Order = 1)]
        public Guid LotId { get; set; }

        /// <summary>
        /// Gets or sets the lot name.
        /// </summary>
        [JsonProperty(Order = 2)]
        public string? LotName { get; set; }

        /// <summary>
        /// Gets or sets the quantity of entities in the lot.
        /// </summary>
        [JsonProperty(Order = 3)]
        [Range(1, LotConstraints.MaxQuantityOfProductionOrders)]
        public int Quantity { get; set; }
    }
}