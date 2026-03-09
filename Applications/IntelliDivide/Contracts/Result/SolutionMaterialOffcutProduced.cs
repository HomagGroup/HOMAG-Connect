#nullable enable

using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Extensions;
using HomagConnect.Base.Contracts.Interfaces;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Describes an offcut produced by the solution.
    /// </summary>
    /// <example>
    /// {
    ///   "materialCode": "P2_Gold_Craft_Oak_19.0",
    ///   "length": 1201.0,
    ///   "width": 567.8,
    ///   "thickness": 19.0,
    ///   "quantity": 2,
    ///   "costs": 5.03,
    ///   "grain": "Crosswise",
    ///   "ids": ["XID-670959", "XID-624742"]
    /// }
    /// </example>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionMaterialOffcutProduced: IDimensionProperties, IMaterialProperties, IHasMaterialCode
    {
        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        /// <example>
        /// { "customProperty": "custom value" }
        /// </example>
        [JsonProperty(Order = 80)]
        [JsonExtensionData]
        public IDictionary<string, object>? AdditionalProperties { get; set; }

        /// <summary>
        /// Gets or sets the total costs.
        /// Unit for metric and imperial unit systems: currency value.
        /// </summary>
        /// <example>5.03</example>
        [JsonProperty(Order = 7)]
        public double Costs { get; set; }

        /// <summary>
        /// Gets or sets the grain direction of the produced offcut.
        /// </summary>
        /// <example>Crosswise</example>
        [JsonProperty(Order = 7)]
        public Grain Grain { get; set; }

        /// <summary>
        /// Gets or sets the material alias of <see cref="MaterialCode"/>.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
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
        /// <example>["XID-670959", "XID-624742"]</example>
        [JsonProperty(Order = 8)]
        public string[]? Ids { get; set; }

        /// <summary>
        /// Gets or sets the offcut length.
        /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>1201.0</example>
        [JsonProperty(Order = 3)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the material code of the produced offcut.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
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
        /// Gets or sets the produced quantity of this offcut.
        /// Unit for metric and imperial unit systems: count.
        /// </summary>
        /// <example>2</example>
        [JsonProperty(Order = 6)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the offcut thickness.
        /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>19.0</example>
        [JsonProperty(Order = 5)]
        public double? Thickness { get; set; }

        /// <summary>
        /// Gets or sets the offcut width.
        /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>567.8</example>
        [JsonProperty(Order = 4)]
        public double? Width { get; set; }
    }
}