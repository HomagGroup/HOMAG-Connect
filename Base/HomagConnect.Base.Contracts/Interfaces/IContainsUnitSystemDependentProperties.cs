using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Defines a unit system for contracts that contain unit-dependent properties.
    /// Properties marked with <see cref="ValueDependsOnUnitSystemAttribute" /> are interpreted according to this value.
    /// </summary>
    public interface IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets or sets the unit system used for properties marked with <see cref="ValueDependsOnUnitSystemAttribute" />.
        /// Use <c>Metric</c> for metric units (for example mm) and <c>Imperial</c> for imperial units (for example inches).
        /// </summary>
        /// <example>Metric</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UnitSystem))]
        UnitSystem UnitSystem { get; set; }
    }
}