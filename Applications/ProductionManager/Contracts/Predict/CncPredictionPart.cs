using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.ProductionManager.Contracts.ProductionEntity;

using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Predict
{
    /// <summary>
    /// Part for CNC prediction.
    /// </summary>
    public class CncPredictionPart : IDimensionProperties, ICncProgramProperties
    {
        /// <summary>
        /// Gets or sets the id of the part.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the quantity how often the part is needed.
        /// </summary>
        [Required]
        [JsonProperty(Order = 2)]
        [Range(1, 10000)]
        public int Quantity { get; set; } = 1;

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

        #region CNC processing

        /// <inheritdoc />
        [JsonProperty(Order = 41)]
        [StringLength(100, MinimumLength = 1)]
        public string? CncProgramName1 { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 42)]
        [StringLength(100, MinimumLength = 1)]
        public string? CncProgramName2 { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 42)]
        [StringLength(100, MinimumLength = 1)]
        public string? CncProgramName3 { get; set; }
        #endregion

        #region IContainsUnitSystemDependentProperties Members

        /// <inheritdoc />
        [JsonProperty(Order = 999)]
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;

        #endregion

        /// <summary>
        /// Converts a production order to an cutting prediction part.
        /// </summary>
        /// <param name="productionOrder"></param>
        public static implicit operator CncPredictionPart(ProductionEntityProductionOrder productionOrder)
        {
            var part = new CncPredictionPart
            {
                Id = productionOrder.Id,
                Quantity = productionOrder.Quantity,

                Length = productionOrder.Length,
                Width = productionOrder.Width,
                Thickness = productionOrder.Thickness,

                UnitSystem = productionOrder.UnitSystem
            };

            return part;
        }
    }
}