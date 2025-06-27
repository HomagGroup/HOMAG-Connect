using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using HomagConnect.MaterialManager.Contracts.Material.Base;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Processing.Optimization
{
    /// <summary>
    /// Tension trim type
    /// </summary>
    [ResourceManager(typeof(TensionTrimTypeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum TensionTrimType
    {
        /// <summary>
        /// Simple tension trim
        /// </summary>
        [Display(Description = "Simple Tension Trim")]
        SimpleTensionTrim,

        /// <summary>
        /// Slot centered between strips
        /// </summary>
        [Display(Description = "Slot Centered Between Strips")] 
        SlotCenteredBetweenStrips,

        /// <summary>
        /// Bridge centered between strips
        /// </summary>
        [Display(Description = "Bridge Centered Between Strips")] 
        BridgeCenteredBetweenStrips
    }
}