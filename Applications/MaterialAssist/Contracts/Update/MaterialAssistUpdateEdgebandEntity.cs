using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialAssist.Contracts.Update
{
    public class MaterialAssistUpdateEdgebandEntity
    {
        /// <summary>
        /// Gets or sets the additional comments.
        /// </summary>
        [StringLength(300)]
        public string? Comments { get; set; } = null;

        /// <summary>
        /// Gets or sets the thickness of the edgeband. The unit depends on the settings of the subscription (metric: mm, imperial:
        /// inch).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter, DecimalPrecision.TwoDecimalPlaces)]
        [Range(0.01, 99.99)]
        public double? CurrentThickness { get; set; } = null;

        /// <summary>
        /// Gets or sets the length of the edgeband. The unit depends on the settings of the subscription (metric: m, imperial:
        /// ft).
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter, DecimalPrecision.TwoDecimalPlaces, DecimalPrecision.TwoDecimalPlaces)]
        [Range(0.1, 9999.99)]
        public double? Length { get; set; } = null;

        /// <summary>
        /// Gets or sets the quantity of the edgeband entity.
        /// </summary>
        [Range(1, 100)]
        public int? Quantity { get; set; } = null;
    }
}