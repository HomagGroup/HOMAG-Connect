using System.Collections.Generic;

using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    /// <summary>
    /// A edgeband type details.
    /// </summary>
    public class EdgebandTypeDetails : EdgebandType
    {
        /// <summary>
        /// Gets or sets the board type inventory.
        /// </summary>
        public ICollection<EdgebandTypeInventory> Inventory { get; set; } = new List<EdgebandTypeInventory>();
        
        /// <summary>
        /// Gets or sets the list of additional images.
        /// </summary>
        public ICollection<ImageInformation> Images { get; set; } = new List<ImageInformation>();
    }
}