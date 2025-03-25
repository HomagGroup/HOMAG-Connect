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


        /// <summary>
        /// Machine number
        /// </summary>
        [JsonProperty("MachineNumber")]
        private string? MachineNumberObsolete
        {
            set
            {
                MachineNumber = value;
            }
        }
        /// <summary>
        /// Machine number
        /// </summary>
        [JsonProperty("MachineName")]
        private string? MachineNameObsolete
        {
            set
            {
                MachineName = value;
            }
        }

        /// <summary>
        /// Type of the machine (CNC, Drilling, etc.)
        /// </summary>
        [JsonProperty("MachineType")]
        private string? MachineTypeObsolete
        {
            set
            {
                MachineType = value;
            }
        }

        /// <summary>
        /// Machine instance Id
        /// </summary>
        [JsonProperty("InstanceId")]
        public string? InstanceIdObsolete
        {
            set
            {
                InstanceId = value;
            }
        }
    }
}