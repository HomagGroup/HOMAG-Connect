using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Cnc
{
    /// <summary>
    /// The milling parameter group.
    /// </summary>
    public class MillingParameterGroup
    {
        /// <summary>
        /// The name of the material group.
        /// </summary>
        [JsonProperty(Order = 1)]
        [Required]
        public string MaterialGroupName { get; set; }

        /// <summary>
        /// The parameters of the group.
        /// </summary>
        [JsonProperty(Order = 3)]
        [Required]
        public MillingParameters Parameters { get; set; } = new();

        /// <summary>
        /// The name of the tool group.
        /// </summary>
        [JsonProperty(Order = 2)]
        [Required]
        public string ToolGroupName { get; set; }
    }
}