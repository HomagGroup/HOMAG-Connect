using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards;

namespace HomagConnect.MaterialManager.Contracts.Request
{
    /// <summary>
    /// Request object to create an Allocation in materialManager.
    /// </summary>
    public class MaterialManagerRequestBoardTypeAllocation
    {
        private const string _DefaultSource = "materialManager";

        /// <summary>
        /// Gets or sets the allocation name.
        /// </summary>
        [Required]
        public string AllocationName { get; set; }

        /// <summary>
        /// Gets or sets the allocation name.
        /// </summary>
        public ICollection<BoardTypeAllocationDetails> AllocationDetails { get; set; } = new List<BoardTypeAllocationDetails>();

        /// <summary>
        /// Gets or sets the allocation type.
        /// </summary>
        [Required]
        public AllocationType AllocationType { get; set; }

        /// <summary>
        /// Gets or sets the allocation comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Gets or sets the allocation created by.
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the allocation creation date.
        /// </summary>
        [Required]
        public DateTimeOffset CreationDate { get; set; }


        /// <summary>
        ///  Gets or sets source of the allocation.
        /// </summary>
        public string Source { get; set; } = _DefaultSource;

        /// <summary>
        /// Gets or sets the allocation workstation.
        /// </summary>
        public string? Workstation { get; set; }
        
    }

    
}
