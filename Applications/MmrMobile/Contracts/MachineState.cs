using HomagConnect.Base.Contracts.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Machine state details
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class MachineState : MachineInformation
    {
        /// <summary>
        /// Duration in hours
        /// </summary>
        [JsonProperty("Duration [h]")]
        [Display(Name = nameof(MmrPropertyDisplayNames.DurationInHours))]
        public double DurationInHours { get; set; }

        /// <summary>
        /// Detailed state Id
        /// </summary>
        [JsonProperty("Detailed State Id")]
        [Display(Name = nameof(MmrPropertyDisplayNames.DetailedStateId))]
        public string? DetailedStateId { get; set; }

        /// <summary>
        /// Translated text of the detailed state
        /// </summary>
        [JsonProperty("Detailed State")]
        [Display(Name = nameof(MmrPropertyDisplayNames.DetailedStateTranslation))]
        public string? DetailedStateTranslation { get; set; }

        /// <summary>
        /// State Id / State group
        /// </summary>
        [JsonProperty("State Id")]
        [Display(Name = nameof(MmrPropertyDisplayNames.StateId))]
        public string? StateId { get; set; }

        /// <summary>
        /// Translation of the state
        /// </summary>
        [JsonProperty("State")]
        [Display(Name = nameof(MmrPropertyDisplayNames.StateTranslation))]
        public string? StateTranslation { get; set; }
    }
}
