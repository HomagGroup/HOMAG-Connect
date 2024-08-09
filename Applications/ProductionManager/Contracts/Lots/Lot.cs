using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Lot data
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// Gets or sets the id of the lot.
        /// </summary>
        [Key]
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the lot.
        /// </summary>
        [JsonProperty(Order = 2)]
        [StringLength(LotConstraints.MaxNameLength, MinimumLength = 1)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LotStatus" /> of the lot.
        /// </summary>
        [JsonProperty(Order = 4)]
        public LotStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the quantity of parts in the lot.
        /// </summary>
        [JsonProperty(Order = 3)]
        [Range(0, LotConstraints.MaxQuantityOfProductionOrders)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Gets or sets the planned production start date of the lot.
        /// </summary>
        [JsonProperty(Order = 30)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the planned production completion date of the lot.
        /// </summary>
        [JsonProperty(Order = 31)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Gets the date that is the earliest delivery date of all production entities in the lot.
        /// </summary>
        [JsonProperty(Order = 32)]
        public DateTimeOffset? EarliestDeliveryDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the lot was last modified.
        /// </summary>
        [JsonProperty(Order = 40)]
        public DateTimeOffset ChangeAt { get; set; } = DateTimeOffset.Now;
    }
}