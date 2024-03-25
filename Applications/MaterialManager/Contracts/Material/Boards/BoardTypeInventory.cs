using System;
using System.Runtime.Serialization;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    public class BoardTypeInventory: IExtensibleDataObject
    {
        public string? OrderNumber { get; set; }

        public string? Code { get; set; }

        public string? Location { get; set; }

        public string? Workstation { get; set; }

        public int Quantity { get; set; }

        public string? AdditionalCommentsBoards { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}
