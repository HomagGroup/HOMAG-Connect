using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Represents the edge banding configuration for a component, including edge material and thickness information for
    /// each side.
    /// </summary>
    /// <remarks>Use this class to specify the edge banding details for the front, back, left, and right sides
    /// of a component, as well as associated thickness values. Edge banding properties are commonly used in cabinetry
    /// and furniture design to define the materials and appearance of panel edges.</remarks>
    public class EdgeBand : ComponentBase, IEdgebandingProperties
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

        #region IEdgebandingProperties

        /// <inheritdoc />
        [JsonProperty(Order = 410)]
        public string? EdgeFront { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 420)]
        public string? EdgeBack { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 450)]
        public string? EdgeDiagram { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 430)]
        public string? EdgeLeft { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 440)]
        public string? EdgeRight { get; set; }

        /// <summary>
        /// Edge Thickness Front
        /// </summary>
        [JsonProperty(Order = 415)]
        public double? EdgeThicknessFront { get; set; }

        /// <summary>
        /// Edge Thickness Back
        /// </summary>
        [JsonProperty(Order = 425)]
        public double? EdgeThicknessBack { get; set; }

        /// <summary>
        /// Edge Thickness Left
        /// </summary>
        [JsonProperty(Order = 435)]
        public double? EdgeThicknessLeft { get; set; }

        /// <summary>
        /// Edge Thickness Right
        /// </summary>
        [JsonProperty(Order = 445)]
        public double? EdgeThicknessRight { get; set; }

        #endregion
    }
}
