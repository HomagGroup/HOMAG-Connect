using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    /// <summary>
    /// </summary>
    [ResourceManager(typeof(ImportModeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum ImportMode
    {
        /// <summary>
        /// Use for initial import or re-sync.
        /// </summary>
        [Display(Description = "Full")]
        Full,

        /// <summary>
        /// Upsert the data you send.
        /// </summary>
        [Display(Description = "Partial")]
        Partial
    }
}