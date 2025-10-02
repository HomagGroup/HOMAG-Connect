using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Update
{
    /// <summary>
    /// Update object to create an Allocation in materialManager.
    /// </summary>
    public class EdgebandTypeAllocationUpdate
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
        /// Customer of the edgeband type allocation.
        /// </summary>
        public string Customer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the allocation edgeband code.
        /// </summary>
        [Required]
        public string EdgebandCode { get; set; } = string.Empty;

        /// <summary>
        /// Order of the edgeband type allocation.
        /// </summary>
        [Required]
        public string Order { get; set; } = string.Empty;

        /// <summary>
        /// Project of the edgeband type allocation.
        /// </summary>
        public string? Project { get; set; } = null;

        /// <summary>
        /// Gets or sets source of the allocation.
        /// </summary>
        public string? Source { get; set; } = null;

        /// <summary>
        /// UsedLength of the edgeband type allocation.
        /// </summary>
        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double UsedLength { get; set; }

        /// <summary>
        /// Gets or sets the allocation workstation.
        /// </summary>
        [Required]
        public string Workstation { get; set; } = string.Empty;
    }
}