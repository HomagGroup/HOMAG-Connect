using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionEntity;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Part for edgebanding prediction.
    /// </summary>
    public class EdgebandingPredictionPart : IEdgebandingProperties, IDimensionsProperties
    {
        /// <summary>
        /// Gets or sets the id of the part.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the quantity how often the part is needed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 13)]
        [Range(1, 10000)]
        public int Quantity { get; set; } = 1;

        #region IEdgebandingProperties Members

        /// <inheritdoc />
        [JsonProperty(Order = 32)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeBack { get; set; }

        /// <inheritdoc />
        public string? EdgeDiagram { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 31)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeFront { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 33)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeLeft { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 34)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeRight { get; set; }

        #endregion

        #region IDimensionsProperties Members

        /// <summary>
        /// Gets or sets the length of the part.
        /// </summary>
        [JsonProperty(Order = 20)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <summary>
        /// Gets or sets the width of the part.
        /// </summary>
        [JsonProperty(Order = 21)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

        /// <summary>
        /// Gets or sets the thickness of the part.
        /// </summary>
        [JsonProperty(Order = 23)]
        [Range(0.1, 500)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Thickness { get; set; }

        #endregion

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        [JsonProperty(Order = 999)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        /// <summary>
        /// Converts a production order to an edgebanding prediction part.
        /// </summary>
        /// <param name="productionOrder"></param>
        public static implicit operator EdgebandingPredictionPart(ProductionEntityProductionOrder productionOrder)
        {
            var part = new EdgebandingPredictionPart
            {
                Id = productionOrder.Id,
                Quantity = productionOrder.Quantity,

                Length = productionOrder.Length,
                Width = productionOrder.Width,
                Thickness = productionOrder.Thickness,

                EdgeDiagram = productionOrder.EdgeDiagram,
                EdgeBack = productionOrder.EdgeBack,
                EdgeFront = productionOrder.EdgeFront,
                EdgeLeft = productionOrder.EdgeLeft,
                EdgeRight = productionOrder.EdgeRight
            };

            return part;
        }
    }
}