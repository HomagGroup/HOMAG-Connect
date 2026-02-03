using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Order item board
    /// </summary>
    public class Board : ComponentBase, IDimensionProperties, IMaterialProperties
    {
        #region ComponentBase Members

        /// <inheritdoc cref="Base" />
        public override Type Type
        {
            get
            {
                return Type.Board;
            }
            set
            {
                // Ignore
            }
        }

        #endregion

        #region IDimensionProperties

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        [JsonProperty(Order = 15)]
        [DefaultValue(0.0)]
        public double? Thickness { get; set; }

        #endregion

        #region IMaterialProperties

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        [JsonProperty(Order = 100)]
        public string? Material { get; set; }

        /// <summary>
        /// Surface Top
        /// </summary>
        [JsonProperty(Order = 120)]
        public string? SurfaceTop { get; set; }

        /// <summary>
        /// Surface Bottom
        /// </summary>
        [JsonProperty(Order = 130)]
        public string? SurfaceBottom { get; set; }

        /// <summary>
        /// Gets or sets the grain.
        /// </summary>
        [JsonProperty(Order = 110)]
        public Grain Grain { get; set; }

        #endregion

        /// <summary>
        /// Gets or sets the total costs.
        /// </summary>
        [JsonProperty(Order = 7)]
        [DefaultValue(0.0)]
        public double Costs { get; set; }
    }
}
