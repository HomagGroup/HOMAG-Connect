using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

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
        RawCut = 0,

        /// <summary>
        /// Finish cut.
        /// </summary>
        FinishCut = 1,

        /// <summary>
        /// No quality preset.
        /// </summary>
        NoQuality = 2
    }
}