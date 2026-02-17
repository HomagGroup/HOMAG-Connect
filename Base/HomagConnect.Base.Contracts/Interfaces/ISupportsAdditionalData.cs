using HomagConnect.Base.Contracts.AdditionalData;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    public interface ISupportsAdditionalData
    {
        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalData))]
        Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets the additional properties configured in the application.
        /// </summary>
        [JsonExtensionData]
        [JsonProperty(Order = 602)]
        [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}