using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class MmrMachine : IExtensibleDataObject, ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Machine number
        /// </summary>
        [JsonProperty("Machine Number")]
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineNumber))]
        public string? MachineNumber { get; set; }

        /// <summary>
        /// Name of the machine
        /// </summary>
        [JsonProperty("Machine Name")]
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineName))]
        public string? MachineName { get; set; }

        /// <summary>
        /// Type of the machine (CNC, Drilling, etc.)
        /// </summary>
        [JsonProperty("Machine Type")]
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineType))]
        public MachineBaseType MachineType { get; set; }


        /// <summary>
        /// Machine instance Id
        /// </summary>
        [JsonProperty("Instance Id")]
        [Display(Name = nameof(MmrPropertyDisplayNames.InstanceId))]
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
        [Display(AutoGenerateField = false)]
        private string? MachineTypeObsolete
        {
            set
            {
                MachineType = Enum.TryParse<MachineBaseType>(value, true, out var parsed) ? parsed : MachineBaseType.Unknown;
            }
        }

        /// <summary>
        /// Machine instance Id
        /// </summary>
        [JsonProperty("InstanceId")]
        [Display(AutoGenerateField = false)]
        public string? InstanceIdObsolete
        {
            set
            {
                InstanceId = value;
            }
        }
    }
}