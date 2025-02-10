using System.Collections.ObjectModel;

using HomagConnect.Base.Contracts.AdditionalData;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Interfaces
{
    public interface ISupportsAdditionalData
    {
        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 602)]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}