using HomagConnect.Base.Contracts.AdditionalData;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace HomagConnect.Base.Contracts.Interfaces
{
    /// <summary>
    /// Defines support for contracts that expose typed additional data entries together with custom JSON extension properties.
    /// </summary>
    /// <remarks>
    /// Use <see cref="AdditionalData" /> for structured attachments such as images, CNC programs, PDFs, textures, or 3D data.
    /// Use <see cref="ISupportsAdditionalProperties.AdditionalProperties" /> for unmapped custom fields that should round-trip through JSON.
    /// </remarks>
    public interface ISupportsAdditionalData : ISupportsAdditionalProperties
    {
        /// <summary>
        /// Gets or sets the typed additional data entries associated with the contract.
        /// </summary>
        /// <remarks>
        /// This collection is intended for strongly typed payloads based on <see cref="AdditionalDataEntity" />.
        /// Examples include files and metadata for images, CNC programs, PDFs, ZIP archives, textures, and 3D data.
        /// </remarks>
        /// <example>
        /// [
        ///   { "type": "Image", "name": "Overview", "downloadUri": "https://example.com/files/overview.png" }
        /// ]
        /// </example>
        [Display(ResourceType = typeof(Resources), Name = nameof(AdditionalData))]
        [JsonProperty(Order = 998)]
        Collection<AdditionalDataEntity>? AdditionalData { get; set; }
    }
}