using System;
using System.ComponentModel.DataAnnotations;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;

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
        public string Name { get; set; }

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
        /// Gets or sets the allocation comments.
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        ///  Gets or sets source of the allocation.
        /// </summary>
        public string Source { get; set; } = _DefaultSource;

        /// <summary>
        /// Gets or sets the allocation type.
        /// </summary>
        [Required]
        public AllocationType AllocationType { get; set; }

        /// <summary>
        /// Gets or sets the allocation workstation.
        /// </summary>
        public string? Workstation { get; set; }

        /// <summary>
        /// Gets or sets AdditionalData.
        /// </summary>
        public AdditionalDataEntity AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the boardType code.
        /// </summary>
        [Required]
        public string BoardTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}
