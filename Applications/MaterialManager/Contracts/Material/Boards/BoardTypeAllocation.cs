#nullable enable
using System.ComponentModel.DataAnnotations;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// Represents the allocation of a board type to a production-related entity.
    /// </summary>
    /// <example>
    /// { "boardCode": "P2_Gold_Craft_Oak_19.0", "quantity": 4 }
    /// </example>
    public class BoardTypeAllocation : Allocation
    {
        /// <summary>
        /// Gets or sets the board code of the allocated board type.
        /// </summary>
        /// <example>P2_Gold_Craft_Oak_19.0</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeAllocationProperties_BoardCode))]
        [Required]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the allocated quantity of boards.
        /// </summary>
        /// <example>4</example>
        [Display(ResourceType = typeof(Resources), Name = nameof(Resources.BoardTypeAllocationProperties_Quantity))]
        public int Quantity { get; set; }
    }
}