using System;

using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    public abstract class Allocation
    {
        private const string _DefaultSource = "materialManager";

        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public string Comments { get; set; }

        public string Source { get; set; } = _DefaultSource;

        public AllocationType AllocationType { get; set; }

        public string Workstation { get; set; }
        
        public AdditionalDataEntity AdditionalData { get; set; }
    }
}
