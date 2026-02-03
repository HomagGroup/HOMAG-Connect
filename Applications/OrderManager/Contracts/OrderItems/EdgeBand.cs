using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Represents a edge band.
    /// </summary>
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
        /// Gets or sets the material.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string Material { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [JsonProperty(Order = 2)]
        [DefaultValue(0.0)]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        [JsonProperty(Order = 3)]
        [DefaultValue(0.0)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        [JsonProperty(Order = 5)]
        [DefaultValue(0.0)]
        public double Costs { get; set; }

        /// <summary>
        /// Gets or sets the length of the edgeband.
        /// </summary>
        [JsonProperty(Order = 22)]
        [DefaultValue(0.0)]
        public double Length { get; set; }

        /// <summary>
        /// Gets or sets the unit system.
        /// </summary>
        [JsonProperty(Order = 999)]
        [DefaultValue(UnitSystem.Metric)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}
