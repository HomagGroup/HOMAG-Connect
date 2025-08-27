using System.Collections.Generic;

using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.MaterialManager.Contracts.Material.Boards;
using HomagConnect.MaterialManager.Contracts.Material.Boards.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Request
{
    /// <summary>
    /// Request object for importing inventory into materialManager
    /// </summary>
    public class ImportInventoryRequest
    {
        /// <summary>
        /// List of allocations on material types
        /// </summary>
        public IEnumerable<BoardTypeAllocation> Allocations { get; set; } = new List<BoardTypeAllocation>();

        /// <summary>
        /// The mode of the import.
        /// </summary>
        public ImportMode ImportMode { get; set; }

        /// <summary>
        /// Instances of the storage system
        /// </summary>
        public IEnumerable<BoardEntity> Instances { get; set; } = new List<BoardEntity>();

        /// <summary>
        /// Type of the inventory data
        /// </summary>
        public InventoryType InventoryType { get; set; }

        /// <summary>
        /// List of material types
        /// </summary>
        public IEnumerable<BoardType> Materials { get; set; } = new List<BoardType>();

        /// <summary>
        /// Name of the storage system
        /// </summary>
        public string StorageSystemName { get; set; } = string.Empty;

        /// <summary>
        /// The unit system of the data
        /// </summary>
        public UnitSystem UnitSystem { get; set; }
    }
}