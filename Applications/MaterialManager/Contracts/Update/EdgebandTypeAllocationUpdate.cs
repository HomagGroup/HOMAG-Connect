using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Edgebands;

namespace HomagConnect.MaterialManager.Contracts.Update
{
    /// <summary>
    /// Update object to create an Allocation in materialManager.
    /// </summary>
    public class EdgebandTypeAllocationUpdate : EdgebandTypeAllocationRequestBase
    {
        /// <summary>
        /// Gets or sets the allocation allocated length.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double AllocatedLength { get; set; }

        /// <summary>
        /// Gets or sets the allocation comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// UsedLength of the edgeband type allocation.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double UsedLength { get; set; }

        /// <summary>
        /// Gets or sets the allocation workstation.
        /// </summary>
        public string? Workstation { get; set; } = null;
    }
}