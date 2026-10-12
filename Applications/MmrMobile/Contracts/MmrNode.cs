using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class MmrNode : ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Timestamp, when the value did change
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Name of the Node
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Node))]
        public string? Node { get; set; }

        /// <summary>
        /// Value of the node
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Value))]
        public string? Value { get; set; }
    }
}
