using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Cnc
{
    /// <summary>
    /// Material with MaterialGroup.
    /// </summary>
    public class MaterialWithGroup : Material.Boards.Material
    {
        /// <summary>
        /// The name of the MaterialGroup, unique per subscription
        /// </summary>
        [JsonProperty(Order = 90)]
        [Required]
        public string GroupName { get; set; }
    }
}