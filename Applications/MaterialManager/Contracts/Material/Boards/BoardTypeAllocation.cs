using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type allocation.
    /// </summary>
    public class BoardTypeAllocation : Allocation
    {
        /// <summary>
        /// BoardCode of the allocation.
        /// </summary>
        [Required]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Quantity of the allocation.
        /// </summary>
        public int Quantity { get; set; }
    }
}