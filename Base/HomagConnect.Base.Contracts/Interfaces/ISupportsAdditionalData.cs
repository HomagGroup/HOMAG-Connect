using HomagConnect.Base.Contracts.AdditionalData;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Defines support for typed additional data entries and custom extension properties.
    /// </summary>
    public interface ISupportsAdditionalData
    {
        /// <summary>
        /// Gets or sets typed additional data entries associated with the contract.
        /// Use this collection for structured payloads such as images, CNC programs, PDFs, or 3D data.
        /// </summary>
        /// <example>
        /// [
        ///   { "type": "Image", "name": "Overview", "downloadUri": "https://example.com/files/overview.png" }
        /// ]
        /// </example>
        [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalData))]
        Collection<AdditionalDataEntity>? AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets additional custom properties.
        /// Any JSON properties not mapped to typed members can be captured here via <c>[JsonExtensionData]</c>.
        /// </summary>
        /// <example>{ "customField1": "value1" }</example>
        [JsonExtensionData]
        [JsonProperty(Order = 602)]
        [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalProperties))]
        public IDictionary<string, object>? AdditionalProperties { get; set; }
    }
}