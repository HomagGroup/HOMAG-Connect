using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Represents a request to create a production lot.
    /// </summary>
    /// <example>
    /// { "name": "Lot-2025-04-01", "startDatePlanned": "2025-04-14T06:00:00+00:00", "completionDatePlanned": "2025-04-14T14:00:00+00:00", "partIds": [ "PART-1001", "PART-1002" ] }
    /// </example>
    public class CreateLotRequest
    {
        /// <summary>
        /// Gets or sets the name of the lot.
        /// </summary>
        /// <example>Lot-2025-04-01</example>
        [JsonProperty(Order = 1)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the planned production start date of the lot.
        /// </summary>
        /// <example>2025-04-14T06:00:00+00:00</example>
        [JsonProperty(Order = 2)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the planned production completion date of the lot.
        /// </summary>
        /// <example>2025-04-14T14:00:00+00:00</example>
        [JsonProperty(Order = 3)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the part identifiers to add to the lot.
        /// </summary>
        /// <example>[ "PART-1001", "PART-1002" ]</example>
        [JsonProperty(Order = 4)]
        public IEnumerable<string> PartIds { get; set; } = [];
    }
}
