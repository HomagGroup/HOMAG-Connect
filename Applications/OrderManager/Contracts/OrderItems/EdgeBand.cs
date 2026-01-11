using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Represents the edge banding configuration for a component, including edge material and thickness information for
    /// each side.
    /// </summary>
    /// <remarks>Use this class to specify the edge banding details for the front, back, left, and right sides
    /// of a component, as well as associated thickness values. Edge banding properties are commonly used in cabinetry
    /// and furniture design to define the materials and appearance of panel edges.</remarks>
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
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        [JsonProperty(Order = 3)]
        public double Thickness { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        [JsonProperty(Order = 5)]
        public double Costs { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the production entity.
        /// </summary>
        [JsonProperty(Order = 22)]
        public int Quantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the unit system.
        /// </summary>
        [JsonProperty(Order = 999)]
        [DefaultValue(UnitSystem.Metric)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}
