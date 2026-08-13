using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MmrMobile.Contracts
{
    /// <summary>
    /// Base information for machine related data (state, counter, ...).
    /// </summary>
    [LocalizationResource(typeof(MmrPropertyDisplayNames))]
    public class MachineInformation : MmrMachine, ISupportsLocalizedSerialization
    {

        /// <summary>
        /// Timestamp of the taken data
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Timestamp))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Shows the requested granularity
        /// </summary>
        [Display(Name = nameof(MmrPropertyDisplayNames.Granularity))]
        public Granularity? Granularity { get; set; }
    }
}
