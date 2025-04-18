﻿using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Machine counter details
    /// </summary>
    public class MachineCounter : MachineInformation
    {
        /// <summary>
        /// Counter value
        /// </summary>
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
        public string? CounterTranslation { get; set; }

        /// <summary>
        /// Related unit to the counter
        /// </summary>
        public string? Unit { get; set; }
    }
}