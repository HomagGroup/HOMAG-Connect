using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations
{
    /// <summary>
    /// Defines the grain of a part, board or offcut.
    /// </summary>
    [ResourceManager(typeof(GrainDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum Grain
    {
        /// <summary>
        /// No grain.
        /// </summary>
        None = 0,

        /// <summary>
        /// Grain along the length.
        /// </summary>
        Lengthwise = 1,

        /// <summary>
        /// Grain along the width.
        /// </summary>
        Crosswise = 2
    }
}