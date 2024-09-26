using System;
using System.Runtime.Serialization;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type inventory.
    /// </summary>
    public class BoardTypeInventory: IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the workstation.
        /// </summary>
        public string? Workstation { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the additional comments for the board.
        /// </summary>
        public string? AdditionalCommentsBoards { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the instance data.
        /// </summary>
        public DateTimeOffset? CreationDate { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}
