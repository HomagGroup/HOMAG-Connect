using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Cnc
{
    /// <summary>
    /// Material with MaterialGroup used for Cnc process parameters.
    /// </summary>
    public class MaterialWithMaterialGroup : Material.Boards.Material
    {
        /// <summary>
        /// The name of the MaterialGroup, unique per subscription
        /// </summary>
        [Key]
        [JsonProperty(Order = 90)]
        [Required]
        public string MaterialGroupName { get; set; }
    }
}