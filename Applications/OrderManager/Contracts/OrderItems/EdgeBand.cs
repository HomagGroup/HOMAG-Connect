using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Edge band of an order, including its material, dimensions, length and costs.
    /// </summary>
    /// <example>
    /// { "type": "EdgeBand", "id": "E1", "material": "ABS_Oak_2.0", "height": 23, "thickness": 2, "costs": 31.2, "length": 18500, "coilQuantity": 1, "unitSystem": "Metric" }
    /// </example>
    public class EdgeBand : Base
    {
        #region ComponentBase Members

        /// <inheritdoc cref="Base" />
        public override Type Type
        {
            get
            {
                return Type.EdgeBand;
            }
            set
            {
                // Ignore
            }
        }

        #endregion

        /// <summary>
        /// Material code of the edge band.
        /// </summary>
        /// <example>ABS_Oak_2.0</example>
        [JsonProperty(Order = 1)]
        public string Material { get; set; } = string.Empty;

        /// <summary>
        /// Height of the edge band.
        /// </summary>
        /// <example>23</example>
        [JsonProperty(Order = 2)]
        [DefaultValue(0.0)]
        public double Height { get; set; }

        /// <summary>
        /// Thickness of the edge band.
        /// </summary>
        /// <example>2</example>
        [JsonProperty(Order = 3)]
        [DefaultValue(0.0)]
        public double Thickness { get; set; }

        /// <summary>
        /// Total costs of the edge band. Defaults to 0.
        /// </summary>
        /// <example>31.2</example>
        [JsonProperty(Order = 5)]
        [DefaultValue(0.0)]
        public double Costs { get; set; }

        /// <summary>
        /// Length of the edge band.
        /// </summary>
        /// <example>18500</example>
        [JsonProperty(Order = 22)]
        [DefaultValue(0.0)]
        public double Length { get; set; }

        /// <summary>
        /// Number of edge band coils. Optional.
        /// </summary>
        /// <example>1</example>
        [JsonProperty(Order = 23)]
        public int? CoilQuantity { get; set; }

        /// <summary>
        /// Unit system of the dimensions. Defaults to <c>Metric</c>.
        /// </summary>
        /// <example>Metric</example>
        [JsonProperty(Order = 999)]
        [DefaultValue(UnitSystem.Metric)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}
