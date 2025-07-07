using System;
using System.Runtime.Serialization;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type allocation.
    /// </summary>
    public class BoardTypeAllocation: Allocation
    {
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BoardTypeCode { get; set; }
    }
}