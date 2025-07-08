using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board type allocation.
    /// </summary>
    public class BoardTypeAllocation : Allocation
    {
        /// <summary>
        /// BoardTypeCode of the allocation.
        /// </summary>
        public string BoardTypeCode { get; set; } = string.Empty;

        /// <summary>
        /// Quantity of the allocation.
        /// </summary>
        public int Quantity { get; set; }
    }
}