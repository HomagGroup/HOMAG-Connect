using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    /// <summary>
    /// Edgeband type allocation.
    /// </summary>
    public class EdgebandTypeAllocation : Allocation, IContainsUnitSystemDependentProperties
    {
        /// <summary>
        /// AllocatedLength of the edgeband type allocation.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double AllocatedLength { get; set; }

        /// <summary>
        /// Customer of the edgeband type allocation.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// EdgebandCode of the edgeband type allocation.
        /// </summary>
        [Required]
        public string EdgebandCode { get; set; } = string.Empty;

        /// <summary>
        /// Order of the edgeband type allocation.
        /// </summary>
        public string Order { get; set; } = string.Empty;

        /// <summary>
        /// Project of the edgeband type allocation.
        /// </summary>
        public string Project { get; set; } = string.Empty;

        /// <summary>
        /// UsedLength of the edgeband type allocation.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double UsedLength { get; set; }

        /// <summary>
        /// UnitSystem of the edgeband type allocation.
        /// </summary>
        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}