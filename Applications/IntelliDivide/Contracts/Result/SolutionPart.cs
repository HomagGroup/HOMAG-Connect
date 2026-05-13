#nullable enable
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.IntelliDivide.Contracts.Common;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides access to part properties.
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionPart : OptimizationBasePart, IContainsUnitSystemDependentProperties, ISupportsLocalizedSerialization
    {
        /// <summary>
        /// Gets the list of patterns in which the part is contained including the quantity of the part in the pattern.
        /// </summary>
        [JsonProperty(Order = 80)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(PatternReferences))]
        public Collection<PatternReference> PatternReferences { get; set; } = [];

        /// <summary>
        /// Gets a link to a preview image of the part.
        /// </summary>
        [JsonProperty(Order = 70)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(Preview))]
        public Uri Preview { get; set; }

        /// <summary>
        /// Gets or sets the name of the generated nesting program for the part.
        /// </summary>
        [JsonProperty(Order = 70)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(ProgramName))]
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
        [JsonProperty(Order = 15)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(Quantity))]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the quantity of plus parts.
        /// </summary>
        [JsonProperty(Order = 16)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(QuantityPlus))]
        public int QuantityPlus { get; set; }

        /// <summary>
        /// Gets the total quantity of parts.
        /// </summary>
        [JsonProperty(Order = 17)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(QuantityTotal))]
        public int QuantityTotal
        {
            get
            {
                return Quantity + QuantityPlus;
            }
            // ReSharper disable once ValueParameterNotUsed
            private set
            {
                // needed for deserialization
            }
        }
    }
}