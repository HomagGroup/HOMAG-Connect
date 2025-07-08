using HomagConnect.Base.Contracts.AdditionalData;
using HomagConnect.Base.Contracts.Enumerations;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    public abstract class Allocation : IExtensibleDataObject
    {
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }

        public string Comments { get; set; }

        public string Source { get; set; }

        public AllocationType AllocationType { get; set; }

        public string Workstation { get; set; }
        
        public AdditionalDataEntity AdditionalData { get; set; }

        #region IExtensibleDataObject Members

        /// <inheritdoc />
        [JsonExtensionData]
        public ExtensionDataObject? ExtensionData { get; set; }

        #endregion
    }
}
