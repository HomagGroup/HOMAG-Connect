using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// repsonse for requesting all active nodes of a machine
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class MmrNodeList : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Number of the machine in Homag-Format
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineNumber))]
        public string? MachineNumber { get; set; }

        /// <summary>
        /// Name of the machine
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineName))]
        public string? MachineName { get; set; } 

        /// <summary>
        /// List of strings / nodenames
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Nodes))]
        public string[]? Nodes { get; set; } 
    }
}