using System.Collections.Generic;

using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    public class BoardCodeWithInventory
    {
        /// <summary>
        /// Gets or sets the board code.
        /// </summary>
        [JsonProperty(Order = 1)]
        public string BoardCode { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total quantity of boards of this type in the inventory.
        /// </summary>
        [JsonProperty(Order = 50)]
        public int? TotalQuantity { get; set; } 

        /// <summary>
        /// Gets or sets the total quantity of boards of this type which have been allocated to a production order.
        /// </summary>
        [JsonProperty(Order = 51)]
        public int? TotalQuantityAllocated { get; set; } 

        /// <summary>
        /// Gets or sets the total quantity of boards of this type which are available in the inventory.
        /// </summary>
        [JsonProperty(Order = 52)]
        public int? TotalQuantityAvailable { get; set; }

        /// <summary>
        /// Gets or sets the total area of boards of this type in the inventory. The unit depends on the settings of the
        /// subscription (metric: m², imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 56)]
        public double? TotalArea { get; set; } 

        /// <summary>
        /// Gets or sets the total area of boards of this type which have been allocated to a production order. The unit depends on
        /// the settings of the subscription (metric: m², imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 57)]
        public double? TotalAreaAllocated { get; set; } 

        /// <summary>
        /// Gets or sets the total area of boards of this type which are available in the inventory. The unit depends on the
        /// settings of the subscription (metric: m², imperial: ft²).
        /// </summary>
        [JsonProperty(Order = 58)]
        public double? TotalAreaAvailable { get; set; }

        /// <summary>
        /// Gets or sets the board type inventory.
        /// </summary>
        [JsonProperty(Order = 82)]
        public ICollection<BoardTypeInventory> Inventory { get; set; } = new List<BoardTypeInventory>();
    }
}