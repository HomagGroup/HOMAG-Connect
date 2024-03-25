using System;
using System.Runtime.Serialization;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    public class BoardTypeAllocation: IExtensibleDataObject
    {
        public string? AllocationComments { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public int? Quantity { get; set; }

        public string? Type { get; set; }

        public string? Workstation { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}