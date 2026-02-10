#nullable enable

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the offcuts produced by the solution
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialOffcutProduced 
    {
        /// <summary>
        /// Gets or sets the total costs.
        /// </summary>
        [JsonProperty(Order = 7)]
        public double Costs { get; set; }

        /// <summary>
        /// Gets or sets the length.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the material code.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string MaterialCode
        {
            get;
            set
            {
                field = value.Trimmed();
            }
        } = string.Empty;

        /// <summary>
        /// Gets or sets the demand.
        /// </summary>
        [JsonProperty(Order = 6)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        [JsonProperty(Order = 5)]
        public double Thickness { get; set; }
        
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        [JsonProperty(Order = 4)]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the grain of the offcut.
        /// </summary>
        [JsonProperty(Order = 7)]
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the offcut ids announced in materialAssist.
        /// </summary>
        [JsonProperty(Order = 8)]
        public string[]? Ids { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}