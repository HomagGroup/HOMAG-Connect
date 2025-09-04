using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations
{
    /// <summary>
    /// </summary>
    [ResourceManager(typeof(InventoryTypeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum InventoryType
    {
        /// <summary>
        /// </summary>
        [Display(Description = "Edgebands")]
        Edgebands,

        /// <summary>
        /// </summary>
        [Display(Description = "Boards")]
        Boards
    }
}