using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HomagConnect.OrderManager.Contracts.OrderItems
{
    /// <summary>
    /// Board of an order, including its material, thickness and costs.
    /// </summary>
    /// <example>
    /// { "type": "Board", "id": "B1", "material": "P2_Gold_Craft_Oak_19.0", "thickness": 19, "grain": "Lengthwise", "costs": 42.5 }
    /// </example>
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
        /// Thickness of the board.
        /// </summary>
        /// <example>19</example>
        [JsonProperty(Order = 15)]
        [DefaultValue(0.0)]
        public double? Thickness { get; set; }

        #endregion

        #region IMaterialProperties

        /// <summary>
        /// Material code of the board. Optional.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        [JsonProperty(Order = 100)]
        public string? Material { get; set; }

        /// <summary>
        /// Surface of the top side. Optional.        /// </summary>
        [JsonProperty(Order = 120)]
        public string? SurfaceTop { get; set; }

        /// <summary>
        /// Surface of the bottom side. Optional.        /// </summary>
        [JsonProperty(Order = 130)]
        public string? SurfaceBottom { get; set; }

        /// <summary>
        /// Grain direction of the board.
        /// </summary>
        [JsonProperty(Order = 110)]
        public Grain Grain { get; set; }

        #endregion

        /// <summary>
        /// Total costs of the board. Defaults to 0.
        /// </summary>
        /// <example>42.5</example>
        [JsonProperty(Order = 7)]
        [DefaultValue(0.0)]
        public double Costs { get; set; }
    }
}
