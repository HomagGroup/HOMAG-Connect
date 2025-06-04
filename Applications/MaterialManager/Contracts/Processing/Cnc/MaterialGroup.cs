using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Cnc
{
    /// <summary>
    /// MaterialGroup used for Cnc process parameters.
    /// </summary>
    public class MaterialGroup
    {
        /// <summary>
        /// The materialcodes contained in the materialgroup
        /// </summary>
        [JsonProperty(Order = 2)]
        public ICollection<string> MaterialCodes { get; set; } = new List<string>();

        /// <summary>
        /// The name of the MaterialGroup, unique per subscription
        /// </summary>
        [Key]
        [JsonProperty(Order = 1)]
        [Required]
        public required string MaterialGroupName { get; set; }
    }
}