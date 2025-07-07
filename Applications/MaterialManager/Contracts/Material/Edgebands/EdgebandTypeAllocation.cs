using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    public class EdgebandTypeAllocation : Allocation
    {
        public int Number { get; set; }

        public string Customer { get; set; }

        public string Project { get; set; }

        public string Order { get; set; }

        public string EdgebandTypeCode { get; set; }
        
        public string AllocatedLength { get; set; }

        public string UsedLength { get; set; }
    }
}
