using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Defines cutting- and nesting-related properties used to describe finished dimensions,
    /// optional secondary cut sizes, label layout, and grain matching template information.
    /// </summary>
    public interface ICuttingProperties
    {
        /// <summary>
        /// Gets or sets the finished length of the part after production.
        /// Unit: mm when <c>UnitSystem.Metric</c> is used; inches when <c>UnitSystem.Imperial</c> is used.
        /// </summary>
        /// <example>720</example>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? FinishLength { get; set; }

        /// <summary>
        /// Gets or sets the finished width of the part after production.
        /// Unit: mm when <c>UnitSystem.Metric</c> is used; inches when <c>UnitSystem.Imperial</c> is used.
        /// </summary>
        /// <example>480</example>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? FinishWidth { get; set; }

        /// <summary>
        /// Gets or sets the label layout to use for the part.
        /// </summary>
        /// <example>Standard</example>
        public string? LabelLayout { get; set; }

        /// <summary>
        /// Gets or sets the grain matching template reference.
        /// Typically contains the template name, position, instance, and option values in a compact string format.
        /// </summary>
        /// <example>2 Parts (2 x 1):1.1:1:1</example>
        public string? Template { get; set; }

        /// <summary>
        /// Gets or sets the length of the secondary cut size.
        /// Unit: mm when <c>UnitSystem.Metric</c> is used; inches when <c>UnitSystem.Imperial</c> is used.
        /// </summary>
        /// <example>700</example>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? SecondCutLength { get; set; }

        /// <summary>
        /// Gets or sets the width of the secondary cut size.
        /// Unit: mm when <c>UnitSystem.Metric</c> is used; inches when <c>UnitSystem.Imperial</c> is used.
        /// </summary>
        /// <example>460</example>
        [ValueDependsOnUnitSystem(BaseUnit.Millimeter)]
        [Range(0.1, 19999.9)]
        public double? SecondCutWidth { get; set; }
    }
}