using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Update
{
    /// <summary>
    /// Update object to create an Allocation in materialManager.
    /// </summary>
    public class BoardTypeAllocationUpdate
    {
        /// <summary>
        /// Gets or sets the allocation name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the allocation created by.
        /// </summary>
        public string? CreatedBy { get; set; } = null;

        /// <summary>
        /// Gets or sets the allocation creation date.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; } = null;

        /// <summary>
        /// Gets or sets the allocation comments.
        /// </summary>
        public string? Comments { get; set; } = null;

        /// <summary>
        ///  Gets or sets source of the allocation.
        /// </summary>
        public string? Source { get; set; } = null;

        /// <summary>
        /// Gets or sets the allocation type.
        /// </summary>
        public AllocationType? AllocationType { get; set; } = null;

        /// <summary>
        /// Gets or sets the allocation workstation.
        /// </summary>
        public string? Workstation { get; set; } = null;

        /// <summary>
        /// Gets or sets the boardType code.
        /// </summary>
        
        public string? BoardTypeCode { get; set; } = null;

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int? Quantity { get; set; } = null;

    }

    
}
