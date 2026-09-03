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

        /// <summary>
        /// Gets the part area.
        /// </summary>
        [JsonProperty(Order = 18)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(PartArea))]
        public double? PartArea { get; set; }

        /// <summary>
        /// Gets the proportional board demand.
        /// </summary>
        [JsonProperty(Order = 19)] 
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(BoardDemandProportional))]
        public double? BoardDemandProportional { get; set; }

        /// <summary>
        /// Gets the board cost.
        /// </summary>
        [JsonProperty(Order = 20)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(BoardCost))]
        public double? BoardCost { get; set; }

        /// <summary>
        /// Gets the total board cost.
        /// </summary>
        [JsonProperty(Order = 21)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(BoardCostTotal))]
        public double? BoardCostTotal { get; set; }

        /// <summary>
        /// Gets the proportional board cost.
        /// </summary>
        [JsonProperty(Order = 22)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(BoardCostProportional))]
        public double? BoardCostProportional { get; set; }

        /// <summary>
        /// Gets the edge band demand for the part.
        /// </summary>
        [JsonProperty(Order = 23)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(EdgebandDemand))]
        public double? EdgebandDemand { get; set; }

        /// <summary>
        /// Gets the edge band cost.
        /// </summary>
        [JsonProperty(Order = 24)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(EdgebandCost))]
        public double? EdgebandCost { get; set; }

        /// <summary>
        /// Gets the edge band total cost.
        /// </summary>
        [JsonProperty(Order = 25)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(EdgebandCostTotal))]
        public double? EdgebandCostTotal { get; set; }

        /// <summary>
        /// Gets the total material cost.
        /// </summary>
        [JsonProperty(Order = 26)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(MaterialCostTotal))]
        public double? MaterialCostTotal { get; set; }

        /// <summary>
        /// Gets the proportional material cost.
        /// </summary>
        [JsonProperty(Order = 27)]
        [Range(0, int.MaxValue)]
        [Display(ResourceType = typeof(SolutionDisplayNames), Name = nameof(MaterialCostProportional))]
        public double? MaterialCostProportional { get; set; }
    }
}