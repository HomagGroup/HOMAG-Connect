using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Rework category
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    [ResourceManager(typeof(ReworkCategoryDisplayNames))]
    public enum ReworkCategory
    {
        /// <summary>
        /// Other
        /// </summary>
        Other,

        /// <summary>
        /// Dividing
        /// </summary>
        Dividing,

        /// <summary>
        /// Edgebanding
        /// </summary>
        Edgebanding,

        /// <summary>
        /// CNC
        /// </summary>
        CNC,

        /// <summary>
        /// Surface
        /// </summary>
        Surface
    }
}