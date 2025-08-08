using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

using HomagConnect.Base.Contracts;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type inventory.
    /// </summary>
    public class BoardTypeInventory : IExtensibleDataObject
    {
        /// <summary>
        /// Gets or sets the additional comments for the board.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_AdditionalCommentsBoards))]
        public string? AdditionalCommentsBoards { get; set; }

        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Code))]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the instance data.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_CreationDate))]
        public DateTimeOffset? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Location))]
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        public string? OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Quantity))]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the workstation.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeInventoryProperties_Workstation))]
        public string? Workstation { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}