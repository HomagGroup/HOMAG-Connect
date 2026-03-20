using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    public interface ISupportsAdditionalProperties  
    {
        /// <summary>
        /// Gets or sets additional custom properties.
        /// Any JSON properties not mapped to typed members can be captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "customField1": "value1" }</example>
        [JsonExtensionData]
        [JsonProperty(Order = 999)]
        [Display(ResourceType = typeof(HomagConnect.Base.Contracts.Resources), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}