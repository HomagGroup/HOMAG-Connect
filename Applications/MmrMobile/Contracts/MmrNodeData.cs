using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.MmrMobile.Contracts
{

    /// <summary>
    /// list of nodeData
    /// returned by the api
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class MmrNodeData : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// machinenumber of the nodedata
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineNumber))]
        public string? MachineNumber { get; set; }

        /// <summary>
        /// Name of the machine
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.MachineName))]
        public string? MachineName { get; set; }

        /// <summary>
        /// List of nodes
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Nodes))]
        public MmrNode[]? Nodes { get; set; }
    }
}
