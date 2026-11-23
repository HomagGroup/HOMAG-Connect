using System.ComponentModel.DataAnnotations;

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
        /// Name of the lot to create. Optional.
        /// </summary>
        /// <example>Lot-2025-04-01</example>
        [JsonProperty(Order = 1)]
        public string? Name { get; set; }

        /// <summary>
        /// Date and time at which production of the lot is planned to start. Optional.
        /// </summary>
        /// <example>2025-04-14T06:00:00+00:00</example>
        [JsonProperty(Order = 2)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Date and time at which production of the lot is planned to be completed. Optional.
        /// </summary>
        /// <example>2025-04-14T14:00:00+00:00</example>
        [JsonProperty(Order = 3)]
        [DisplayFormat(DataFormatString = "{0:g}")]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Identifiers of the parts to add to the lot. Defaults to an empty list.
        /// </summary>
        /// <example>[ "PART-1001", "PART-1002" ]</example>
        [JsonProperty(Order = 4)]
        public IEnumerable<string> PartIds { get; set; } = [];
    }
}
