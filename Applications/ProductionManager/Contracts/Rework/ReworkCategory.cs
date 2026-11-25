using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Rework
{
    /// <summary>
    /// Classifies a rework by the production step it relates to. Serialized as the member name.
    /// </summary>
    [JsonConverter(typeof(TolerantEnumConverter))]
    [ResourceManager(typeof(ReworkCategoryDisplayNames))]
    public enum ReworkCategory
    {
        /// <summary>
        /// Use when none of the other categories applies.
        /// </summary>
        Other,

        /// <summary>
        /// The issue occurred during dividing (cutting).
        /// </summary>
        Dividing,

        /// <summary>
        /// The issue occurred during edgebanding.
        /// </summary>
        Edgebanding,

        /// <summary>
        /// The issue occurred during CNC processing.
        /// </summary>
        CNC,

        /// <summary>
        /// The issue affects the surface of the part.
        /// </summary>
        Surface
    }
}