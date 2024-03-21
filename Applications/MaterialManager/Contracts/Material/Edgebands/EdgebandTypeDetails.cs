using System.Collections.Generic;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    public class EdgebandTypeDetails : EdgebandType
    {
        public ICollection<EdgebandTypeInventory> Inventory { get; set; } = new List<EdgebandTypeInventory>();
    }
}