using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Represents a production lot with scheduling and quantity information.
    /// <see href="https://docs.homag.cloud/docs/en/productionmanager-tutorial-lot-generation">Learn more about lot generation </see>.
    /// </summary>
    /// <example>
    /// { "id": "7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c", "name": "Lot-2025-04-01", "quantityOfParts": 24, "status": "Planned", "startDatePlanned": "2025-04-14T06:00:00+00:00", "completionDatePlanned": "2025-04-14T14:00:00+00:00", "earliestDeliveryDatePlanned": "2025-04-16T00:00:00+00:00", "changedAt": "2025-04-09T08:15:00+00:00" }
    /// </example>
    public class Lot
    {
        /// <summary>
        /// Unique identifier of the lot, assigned when the lot is created.
        /// </summary>
        /// <example>7d6d4f52-6a9c-4bd5-8e9b-0d7d58ec7d8c</example>
        [Key]
        [JsonProperty(Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the lot. Optional. If provided, it must be between 1 and 100 characters long.
        /// </summary>
        /// <example>Lot-2025-04-01</example>
        [JsonProperty(Order = 2)]
        [StringLength(LotConstraints.MaxNameLength, MinimumLength = 1)]
        public string? Name { get; set; }

        /// <summary>
        /// Current <see cref="LotStatus" /> of the lot.
        /// </summary>
        /// <example>Planned</example>
        [JsonProperty(Order = 4)]
        public LotStatus Status { get; set; }

        /// <summary>
        /// Number of parts in the lot. Allowed range: 0 to 100000.
        /// </summary>
        /// <example>24</example>
        [JsonProperty(Order = 3)]
        [Range(0, LotConstraints.MaxQuantityOfProductionOrders)]
        public int QuantityOfParts { get; set; }

        /// <summary>
        /// Date and time at which production of the lot is planned to start. Optional.
        /// </summary>
        /// <example>2025-04-14T06:00:00+00:00</example>
        [JsonProperty(Order = 30)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.Date)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Date and time at which production of the lot is planned to be completed. Optional.
        /// </summary>
        /// <example>2025-04-14T14:00:00+00:00</example>
        [JsonProperty(Order = 31)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.Date)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Earliest planned delivery date across all production entities in the lot. Optional.
        /// </summary>
        /// <example>2025-04-16T00:00:00+00:00</example>
        [JsonProperty(Order = 32)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTimeOffset? EarliestDeliveryDatePlanned { get; set; }

        /// <summary>
        /// Date and time the lot was last changed.
        /// </summary>
        /// <example>2025-04-09T08:15:00+00:00</example>
        [JsonProperty(Order = 40)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        [DataType(DataType.DateTime)]
        public DateTimeOffset ChangedAt { get; set; } = DateTimeOffset.Now;
    }
}
