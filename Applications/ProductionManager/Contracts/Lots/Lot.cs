using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Represents a production lot with scheduling and quantity information.
    /// </summary>
    /// <example>
    /// { "id": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "name": "Lot-2025-04-01", "quantityOfParts": 24, "status": "Planned", "startDatePlanned": "2025-04-14T06:00:00+00:00", "completionDatePlanned": "2025-04-14T14:00:00+00:00", "earliestDeliveryDatePlanned": "2025-04-16T00:00:00+00:00", "changedAt": "2025-04-09T08:15:00+00:00" }
    /// </example>
    public class Lot
    {
        /// <summary>
        /// Gets or sets the unique identifier of the lot.
        /// </summary>
        /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
        [Key]
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the lot.
        /// </summary>
        /// <example>Lot-2025-04-01</example>
        [JsonProperty(Order = 2)]
        [StringLength(LotConstraints.MaxNameLength, MinimumLength = 1)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the current <see cref="LotStatus" /> of the lot.
        /// </summary>
        /// <example>Planned</example>
        [JsonProperty(Order = 4)]
        public LotStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the quantity of parts in the lot.
        /// </summary>
        /// <example>24</example>
        [JsonProperty(Order = 3)]
        [Range(0, LotConstraints.MaxQuantityOfProductionOrders)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Gets or sets the planned production start date of the lot.
        /// </summary>
        /// <example>2025-04-14T06:00:00+00:00</example>
        [JsonProperty(Order = 30)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the planned production completion date of the lot.
        /// </summary>
        /// <example>2025-04-14T14:00:00+00:00</example>
        [JsonProperty(Order = 31)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the earliest planned delivery date across all production entities in the lot.
        /// </summary>
        /// <example>2025-04-16T00:00:00+00:00</example>
        [JsonProperty(Order = 32)]
        public DateTimeOffset? EarliestDeliveryDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the timestamp when the lot was last modified.
        /// </summary>
        /// <example>2025-04-09T08:15:00+00:00</example>
        [JsonProperty(Order = 40)]
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;
    }
}