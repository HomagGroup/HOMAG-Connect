using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Interface for objects which contain properties that are dependent on the unit system.
    /// </summary>
    public interface IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// Gets or sets the unit system. Properties which are dependent on the unit system should be updated when this property is
        /// changed. They are marked with the <see cref="ValueDependsOnUnitSystemAttribute" />.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.UnitSystem))]
        UnitSystem UnitSystem { get; set; }
    }
}