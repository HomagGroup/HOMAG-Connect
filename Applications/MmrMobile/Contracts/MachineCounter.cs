using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Machine counter details
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class MachineCounter : MachineInformation
    {
        /// <summary>
        /// Counter value
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Value))]
        public double Value { get; set; }

        /// <summary>
        /// Counter Id
        /// </summary>
        [JsonProperty("Counter Id")]
        public string? CounterId { get; set; }

        /// <summary>
        /// Translated text of the counter
        /// </summary>
        [JsonProperty("Counter")]
        [Display(Name = nameof(MmrPropertyDisplayNames.CounterTranslation))]
        public string? CounterTranslation { get; set; }

        /// <summary>
        /// Related unit to the counter
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Unit))]
        public string? Unit { get; set; }
    }
}
