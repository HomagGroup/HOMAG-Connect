using HomagConnect.IntelliDivide.Contracts.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides access to part properties.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionPart : OptimizationBasePart
    {
        /// <summary>
        /// Gets a link to a preview image of the part.
        /// </summary>
        [JsonProperty(Order = 70)]
        public Uri Preview { get; set; }

        /// <summary>
        /// Gets the quantity of parts.
        /// </summary>
        [JsonProperty(Order = 10)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the quantity of plus parts.
        /// </summary>
        [JsonProperty(Order = 11)]
        public int QuantityPlus { get; set; }

        /// <summary>
        /// Gets the total quantity of parts.
        /// </summary>
        [JsonProperty(Order = 12)]
        public int QuantityTotal { get; set; }
        
        /// <summary>
        /// Gets a path that can be used to uniquely identify the part in the zip file.
        /// <remarks>Only useful for Nesting optimizations.</remarks>
        /// </summary>
        [JsonProperty(Order = 70)]
        public string ZipIdentifier { get; set; }

        /// <summary>
        /// Gets the list of patterns in which the part is contained including the quantity of the part in the pattern.
        /// </summary>
        [JsonProperty(Order = 80)]
        public Collection<PatternReference> PatternReferences { get; set; } 
    }
}