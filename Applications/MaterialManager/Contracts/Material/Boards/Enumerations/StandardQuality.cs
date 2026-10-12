using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    /// <summary>
    /// Specifies the expected cut quality for board processing, such as a raw cut, a finish cut, or no preset.
    /// </summary>
    [ResourceManager(typeof(StandardQualityDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum StandardQuality
    {
        /// <summary>
        /// Raw cut.
        /// </summary>
        [Display(Description = "Raw Cut")]
        RawCut = 0,

        /// <summary>
        /// Finish cut.
        /// </summary>
        [Display(Description = "Finish Cut")]
        FinishCut = 1,

        /// <summary>
        /// No quality preset.
        /// </summary>
        [Display(Description = "No Quality")] 
        NoQuality = 2
    }
}