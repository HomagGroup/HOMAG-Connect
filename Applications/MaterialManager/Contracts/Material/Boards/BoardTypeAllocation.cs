using System;
using System.Runtime.Serialization;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type allocation.
    /// </summary>
    public class BoardTypeAllocation: IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the allocation comments.
        /// </summary>
        public string? AllocationComments { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the instance data.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the workstation.
        /// </summary>
        public string? Workstation { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}