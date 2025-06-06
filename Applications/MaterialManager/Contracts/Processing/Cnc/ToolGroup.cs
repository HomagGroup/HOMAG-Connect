﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Processing.Cnc
{
    /// <summary>
    /// ToolGroup used for Cnc process parameters.
    /// </summary>
    public class ToolGroup
    {
        /// <summary>
        /// The name of the ToolGroup, unique per subscription
        /// </summary>
        [Key]
        [JsonProperty(Order = 1)]
        [Required]
        public string ToolGroupName { get; set; }

        /// <summary>
        /// The toolIds contained in the toolgroup
        /// </summary>
        [JsonProperty(Order = 2)]
        public ICollection<string> ToolIds { get; set; } = new List<string>();
    }
}