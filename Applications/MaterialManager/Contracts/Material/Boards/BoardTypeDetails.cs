using System.Collections.Generic;

using HomagConnect.MaterialManager.Contracts.Material.Base;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// The board type details.
    /// </summary>
    public class BoardTypeDetails : BoardType
    {
        /// <summary>
        /// Gets or sets the board type allocations.
        /// </summary>
        [JsonProperty(Order = 83)]
        public ICollection<BoardTypeAllocation> Allocations { get; set; } = new List<BoardTypeAllocation>();

        /// <summary>
        /// Gets or sets the list of additional images.
        /// </summary>
        [JsonProperty(Order = 81)]
        public ICollection<ImageInformation> Images { get; set; } =
            new List<ImageInformation>(); // TODO: Restructure when adding additional data.

        /// <summary>
        /// Gets or sets the board type inventory.
        /// </summary>
        [JsonProperty(Order = 82)]
        public ICollection<BoardTypeInventory> Inventory { get; set; } = new List<BoardTypeInventory>();
    }
}