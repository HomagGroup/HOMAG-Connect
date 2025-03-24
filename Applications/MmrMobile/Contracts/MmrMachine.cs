using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public class MmrMachine : IExtensibleDataObject
    {
        /// <summary>
        /// Machine number
        /// </summary>
        [JsonProperty("Machine Number")]
        public string? MachineNumber { get; set; }

        /// <summary>
        /// Name of the machine
        /// </summary>
        [JsonProperty("Machine Name")]
        public string? MachineName { get; set; }

        /// <summary>
        /// Type of the machine (CNC, Drilling, etc.)
        /// </summary>
        [JsonProperty("Machine Type")]
        public string? MachineType { get; set; }


        /// <summary>
        /// Machine instance Id
        /// </summary>
        [JsonProperty("Instance Id")]
        public string? InstanceId { get; set; }

        public ExtensionDataObject? ExtensionData { get; set; }
    }
}