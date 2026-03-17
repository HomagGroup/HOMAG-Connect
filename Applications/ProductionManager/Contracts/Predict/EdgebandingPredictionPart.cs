using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionItems;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Represents a part used as input for edgebanding duration prediction.
    /// </summary>
    /// <example>
    /// {
    ///   "id": "PART-10",
    ///   "quantity": 2,
    ///   "edgeFront": "EB_White_1mm",
    ///   "edgeBack": "EB_White_1mm",
    ///   "edgeLeft": "EB_White_1mm",
    ///   "edgeRight": "EB_White_1mm",
    ///   "length": 720,
    ///   "width": 480,
    ///   "thickness": 19.0,
    ///   "unitSystem": "Metric"
    /// }
    /// </example>
    public class EdgebandingPredictionPart : IEdgebandingProperties, IDimensionProperties, IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets or sets the identifier of the part.
        /// </summary>
        /// <example>PART-10</example>
        [JsonProperty(Order = 1)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the required quantity of the part. Must be between 1 and 10,000.
        /// </summary>
        /// <example>2</example>
        [Required]
        [JsonProperty(Order = 2)]
        [Range(1, 10000)]
        public int Quantity { get; set; } = 1;

        #region IEdgebandingProperties Members

        /// <inheritdoc />
        [JsonProperty(Order = 30)]
        public string? EdgeDiagram { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 32)]
        [StringLength(50, MinimumLength = 1)]
        public string? EdgeBack { get; set; }

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

        /// <inheritdoc />
        [JsonProperty(Order = 20)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Length { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 21)]
        [Range(0.1, 9999.9)]
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        public double? Width { get; set; }

        /// <inheritdoc />
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
        public static implicit operator EdgebandingPredictionPart(Part productionOrder)
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
                EdgeRight = productionOrder.EdgeRight,

                UnitSystem = productionOrder.UnitSystem
            };

            return part;
        }
    }
}