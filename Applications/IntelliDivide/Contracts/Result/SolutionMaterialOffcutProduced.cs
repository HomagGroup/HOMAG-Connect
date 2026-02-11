#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes the offcuts produced by the solution
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialOffcutProduced: IDimensionProperties, IMaterialProperties, IHasMaterialCode
    {
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the total costs.
        /// </summary>
        [JsonProperty(Order = 7)]
        public double Costs { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 7)]
        public Grain Grain { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string? Material
        {
            get
            {
                return MaterialCode;
            }
            set
            {
                MaterialCode = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the offcut ids announced in materialAssist.
        /// </summary>
        [JsonProperty(Order = 8)]
        public string[]? Ids { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 3)]
        public double? Length { get; set; }

        /// <inheritdoc />
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
        public double? Thickness { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 4)]
        public double? Width { get; set; }
    }
}