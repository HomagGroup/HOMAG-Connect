using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Cutting / Nesting related properties
    /// </summary>
    public interface ICuttingProperties
    {
        /// <summary>
        /// Gets or sets the finish length.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? FinishLength { get; set; }

        /// <summary>
        /// Gets or sets the finish width.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? FinishWidth { get; set; }

        /// <summary>
        /// Gets or sets the label layout.
        /// </summary>
        public string? LabelLayout { get; set; }

        /// <summary>
        /// Gets or set the grain matching template.
        /// </summary>
        public string? Template { get; set; }

        /// <summary>
        /// Gets or sets the 2. Cut size length.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? SecondCutLength { get; set; }

        /// <summary>
        /// Gets or sets the 2. Cut size width.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? SecondCutWidth { get; set; }
    }
}