using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Lots
{
    /// <summary>
    /// Object Model used for creating a lot
    /// </summary>
    public class CreateLotRequest
    {
        /// <summary>
        /// Gets or sets the name of the lot.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the planned production start date of the lot.
        /// </summary>
        [JsonProperty(Order = 2)]
        public DateTimeOffset? StartDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the planned production completion date of the lot.
        /// </summary>
        [JsonProperty(Order = 3)]
        public DateTimeOffset? CompletionDatePlanned { get; set; }

        /// <summary>
        /// Gets or sets the part ids which will be added in the lot.
        /// </summary>
        [JsonProperty(Order = 4)]
        public IEnumerable<string> PartIds { get; set; } = [];
    }
}
