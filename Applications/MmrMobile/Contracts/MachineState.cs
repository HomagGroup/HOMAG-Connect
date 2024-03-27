using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Machine state details
    /// </summary>
    public class MachineState : MachineInformation
    {
        /// <summary>
        /// Duration in hours
        /// </summary>
        [JsonProperty("Duration [h]")]
        public double DurationInHours { get; set; }

        /// <summary>
        /// Machine instance Id
        /// </summary>
        [JsonProperty("Instance Id")]
        public string? InstanceId { get; set; }

        /// <summary>
        /// Detailed state Id
        /// </summary>
        [JsonProperty("Detailed State Id")]
        public string? DetailedStateId { get; set; }

        /// <summary>
        /// Translated text of the detailed state
        /// </summary>
        [JsonProperty("Detailed State")]
        public string? DetailedStateTranslation { get; set; }

        /// <summary>
        /// State Id / State group
        /// </summary>
        [JsonProperty("State Id")]
        public string? StateId { get; set; }

        /// <summary>
        /// Translation of the state
        /// </summary>
        [JsonProperty("State")]
        public string? StateTranslation { get; set; }
    }
}