using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Enumerations;
using HomagConnect.Base.Contracts.Interfaces;
using HomagConnect.MaterialManager.Contracts.Material.Base;

namespace HomagConnect.MaterialManager.Contracts.Material.Edgebands
{
    public class EdgebandTypeAllocation : Allocation, IContainsUnitSystemDependentProperties
    {
        public string Customer { get; set; }

        public string Project { get; set; }

        public string Order { get; set; }

        public string EdgebandTypeCode { get; set; }

        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double AllocatedLength { get; set; }

        [ValueDependsOnUnitSystem(BaseUnit.Meter)]
        public double UsedLength { get; set; }

        public UnitSystem UnitSystem { get; set; } = UnitSystem.Metric;
    }
}
