#nullable enable
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.IntelliDivide.Contracts.Common;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides access to part properties.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionPart : OptimizationBasePart, IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets the list of patterns in which the part is contained including the quantity of the part in the pattern.
        /// </summary>
        [JsonProperty(Order = 80)]
        public Collection<PatternReference> PatternReferences { get; set; } = [];

        /// <summary>
        /// Gets a link to a preview image of the part.
        /// </summary>
        [JsonProperty(Order = 70)]
        public Uri Preview { get; set; }

        /// <summary>
        /// Gets or sets the name of the generated nesting program for the part.
        /// </summary>
        [JsonProperty(Order = 70)]
        public string ProgramName
        {
            get;
            set
            {
                field = value.Trimmed();
            }
        } = string.Empty;

        /// <summary>
        /// Gets the quantity of parts.
        /// </summary>
        [JsonProperty(Order = 10)]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the quantity of plus parts.
        /// </summary>
        [JsonProperty(Order = 11)]
        [Range(0, int.MaxValue)]
        public int QuantityPlus { get; set; }

        /// <summary>
        /// Gets the total quantity of parts.
        /// </summary>
        [JsonProperty(Order = 12)]
        [Range(0, int.MaxValue)]
        public int QuantityTotal { get; set; }
    }
}