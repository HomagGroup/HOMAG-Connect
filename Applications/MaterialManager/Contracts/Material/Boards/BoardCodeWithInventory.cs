using System.Collections.Generic;
using System.Runtime.Serialization;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Interfaces;
using Newtonsoft.Json;

namespace HomagConnect.MaterialManager.Contracts.Material.Boards
{
    /// <summary>
    /// A board code with inventory.
    /// </summary>
    public class BoardCodeWithInventory : IBoardCodeWithInventory, IExtensibleDataObject
    {
        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion

        #region IBoardCodeWithInventory Members

        /// <inheritdoc />
        [JsonProperty(Order = 1)]
        public string BoardCode { get; set; } = string.Empty;

        /// <inheritdoc />
        [JsonProperty(Order = 50)]
        public int? TotalQuantityInInventory { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 51)]
        public int? TotalQuantityAllocated { get; set; }

        /// <inheritdoc />
        [JsonProperty(Order = 52)]
        public int? TotalQuantityAvailable { get; set; }

        [JsonProperty(Order = 56)]
        public double? TotalAreaInventory { get; }

        /// <inheritdoc />
        [JsonProperty(Order = 57)]
        public double? TotalAreaAllocated { get; set; }

        [JsonProperty(Order = 58)]
        public double? TotalAreaAvailable { get; set; }

        [JsonProperty(Order = 82)]
        public ICollection<BoardTypeInventory> Inventory { get; set; } = new List<BoardTypeInventory>();

        #endregion
    }
}