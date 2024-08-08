using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Lot
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// Gets or sets the id of the lot.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the lot.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LotStatus" /> of the lot
        /// </summary>
        public LotStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the quantity of parts in the lot.
        /// </summary>
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Gets or sets the planned production start date of the lot.
        /// </summary>
        [JsonProperty(Order = 30)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets the date that is the earliest delivery date of all production entities in the lot.
        /// </summary>
        [JsonProperty(Order = 118)]
        public DateTimeOffset? DeliveryDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the lot was last modified.
        /// </summary>
        public DateTimeOffset ChangeAt { get; set; }
    }
}