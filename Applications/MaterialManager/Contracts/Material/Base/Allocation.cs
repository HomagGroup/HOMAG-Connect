using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;
using System;
using System.Runtime.Serialization;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    public abstract class Allocation : IExtensibleDataObject
    {
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public string Comments { get; set; }

        public string Source { get; set; }

        public AllocationType AllocationType { get; set; }

        public string Workstation { get; set; }
        
        public AdditionalDataEntity AdditionalData { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}
