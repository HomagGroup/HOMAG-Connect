using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    /// <summary>
    /// The cutting standard quality.
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