using System;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Client.Models
{
    public class MachineInformation
    {
        /// <summary>
        /// Machine number
        /// </summary>
        [JsonProperty("Machine Number")]
        public string MachineNumber { get; set; }

        /// <summary>
        /// Name of the machine
        /// </summary>
        [JsonProperty("Machine Name")]
        public string MachineName { get; set; }

        /// <summary>
        /// Type of the machine (CNC, Drilling, etc.)
        /// </summary>
        [JsonProperty("Machine Type")]
        public string MachineType { get; set; }

        /// <summary>
        /// Timestamp of the taken data
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}