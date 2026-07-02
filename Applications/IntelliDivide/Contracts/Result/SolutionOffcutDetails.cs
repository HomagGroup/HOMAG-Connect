using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Result
{
    /// <summary>
    /// Provides information about a specific offcut.
    /// </summary>
    /// <example>
    /// {
    ///   "id": "XID-1000000",
    ///   "quantity": 3,
    ///   "length": 1201.0,
    ///   "width": 567.8,
    /// }
    /// </example>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class SolutionOffcutDetails
    {
        /// <summary>
        /// Gets or sets the offcut id.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the offcut quantity.
        /// </summary>
        [JsonProperty(Order = 2)]
        public int Quantity  { get; set; }

        /// <summary>
        /// Gets or sets the offcut length.
        /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>1201.0</example>
        [JsonProperty(Order = 3)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the offcut length.
        /// Unit: millimeters for <see cref="UnitSystem.Metric"/> and inches for <see cref="UnitSystem.Imperial"/>.
        /// </summary>
        /// <example>1201.0</example>
        [JsonProperty(Order = 4)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }
    }
}
